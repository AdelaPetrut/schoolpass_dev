using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Newtonsoft.Json;
using Solutrack.Notifications.PushNotifications.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solutrack.Notifications.PushNotifications.Contracts.Enums;

namespace Solutrack.Notifications.PushNotifications.Contracts.Services
{
    public class PushNotificationsService
    {
        private NotificationHubClient hub;

        public PushNotificationsService(NotificationHubClient hub)
        {
            this.hub = hub;
        }

        private async Task<Response<string>> GetRegistrationIdForDeviceAsync(string handle)
        {
            Response<string> response = new Response<string>();
            try
            {
                if (!string.IsNullOrEmpty(handle))
                {
                    var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);

                    foreach (RegistrationDescription registration in registrations)
                    {
                        if (response.Value == null)
                        {
                            response.Value = registration.RegistrationId;
                        }
                        else
                        {
                            await hub.DeleteRegistrationAsync(registration);
                        }
                    }
                }

                if (response.Value == null)
                    response.Value = await hub.CreateRegistrationIdAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Constants.GENERIC_ERROR_MESSAGE + ex.Message;
            }
            return response;
        }

        public async Task<Response<string>> RegisterDeviceAsync(Platform platform, string deviceToken, string username, List<int> deviceIds)
        {
            Response<string> response = new Response<string>();

            try
            {
                var registrationIdResponse = await GetRegistrationIdForDeviceAsync(deviceToken);
                if (registrationIdResponse.IsSuccess == false) return registrationIdResponse;

                RegistrationDescription registration = null;
                switch (platform)
                {
                    case Enums.Platform.apns:
                        var alertTemplate = string.Format("{{\"aps\":{{\"alert\":\"$({0})\" }}, \"base64Data\" : \"$({1})\", \"messageType\" : \"$({2})\"}}",
                            Constants.NOTIFICATION_MESSAGE_KEY, Constants.BASE64_MESSAGE_KEY, Constants.MESSAGE_TYPE);
                        registration = new AppleTemplateRegistrationDescription(deviceToken, alertTemplate);
                        break;
                    case Enums.Platform.gcm:
                        var messageTemplate = string.Format("{{\"data\":{{\"message\":\"$({0})\", \"base64Data\" : \"$({1})\", \"messageType\" : \"$({2})\"}}}}",
                            Constants.NOTIFICATION_MESSAGE_KEY,
                            Constants.BASE64_MESSAGE_KEY, Constants.MESSAGE_TYPE);
                        registration = new GcmTemplateRegistrationDescription(deviceToken, messageTemplate);
                        break;
                    default:
                        return null;
                }

                registration.RegistrationId = registrationIdResponse.Value;
                // add check if user is allowed to add these tags
                if (deviceIds != null)
                    registration.Tags = new HashSet<string>(deviceIds.Select(s => s.ToString()));
                if (!string.IsNullOrEmpty(username))
                    registration.Tags.Add(username);

                var registrationResponse = await hub.CreateOrUpdateRegistrationAsync(registration);
                if (registrationResponse == null)
                {
                    response.IsSuccess = false;
                    response.Message = Constants.GENERIC_ERROR_REGISTER_DEVICE;
                }
                response.Value = registrationIdResponse.Value;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Constants.GENERIC_ERROR_MESSAGE + ex.Message;
            }

            return response;
        }

        public async Task<Response> UnregisterDeviceAsync(string id)
        {
            Response response = new Response();
            try
            {
                await hub.DeleteRegistrationAsync(id);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Constants.GENERIC_ERROR_MESSAGE + ex.Message;
            }
            return response;
        }

        private async Task<Response> SendGenericNotificationAsync(int deviceId, IDeviceMessage message, MessageType messageType)
        {
            Response response = new Response();
            try
            {
                if (deviceId <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = Constants.INVALID_DEVICE_ID;
                    return response;
                }
                if (message == null)
                {
                    response.IsSuccess = false;
                    response.Message = Constants.INVALID_MESSSAGE;
                    return response;
                }
                string base64Message = JsonConvert.SerializeObject(message).EncodeToBase64String();
                var notification = new Dictionary<string, string> {
                    { Constants.NOTIFICATION_MESSAGE_KEY, Constants.NOTIFICATION_NAME },
                    { Constants.MESSAGE_TYPE, Enum.GetName(typeof(MessageType), MessageType.device_ack) },
                    { Constants.BASE64_MESSAGE_KEY, base64Message }
                };
                var result = await hub.SendTemplateNotificationAsync(notification, deviceId.ToString());
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = Constants.GENERIC_ERROR_MESSAGE;
                }
                switch (result.State)
                {
                    case NotificationOutcomeState.Abandoned:
                        response.IsSuccess = false;
                        response.Message = Constants.NOTIFICATION_ABANDONED;
                        break;
                    case NotificationOutcomeState.Cancelled:
                        response.IsSuccess = false;
                        response.Message = Constants.NOTIFICATION_CANCELLED;
                        break;
                    case NotificationOutcomeState.NoTargetFound:
                        response.IsSuccess = false;
                        response.Message = Constants.NOTIFICATION_NO_TARGET_FOUND;
                        break;
                    default:
                        response.IsSuccess = true;
                        response.Message = Constants.NOTIFICATION_SENT_OR_IS_PROCESSED;
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Constants.GENERIC_ERROR_MESSAGE + ex.Message;
            }
            return response;
        }

        public async Task<Response> SendNotificationAsync(int deviceId, AcknowledgmentDeviceMessage message)
        {
            return await SendGenericNotificationAsync(deviceId, message, MessageType.device_ack);
        }

        public async Task<Response> SendNotificationAsync(int deviceId, InformationDeviceMessage message)
        {
            return await SendGenericNotificationAsync(deviceId, message, MessageType.device_info);
        }

        public async Task<Response> SendNotificationAsync(int deviceId, EventDeviceMessage message)
        {
            return await SendGenericNotificationAsync(deviceId, message, MessageType.device_event);
        }
    }
}
