using Solutrack.Notifications.Api.Models;
using Solutrack.Notifications.PushNotifications;
using Solutrack.Notifications.PushNotifications.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static Solutrack.Notifications.PushNotifications.Contracts.Enums;

namespace Solutrack.Notifications.Api.Controllers
{
    [RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        [HttpGet]
        [Route(RouteConstants.API.Notification.GetRegistrationId)]
        public async Task<IHttpActionResult> PostCheckAndReturnRegistrationId(string handle = null)
        {
            try
            {
                var response = await PushNotifications.PushNotificationFacade.Instance.Service.GetRegistrationIdForDeviceAsync(handle);
                if (response.IsSuccess) return Ok(response);
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route(RouteConstants.API.Notification.RegisterDevice)]
        public async Task<IHttpActionResult> PutRegisterDeviceAsync(string id, DeviceRegistrationModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest();
                if (model == null) return BadRequest();

                var registrationResponse = await PushNotificationFacade.Instance.Service.RegisterDeviceAsync(id, new DeviceRegistration
                {
                    DeviceToken = model.Handle,
                    Platform = (Platform)Enum.Parse(typeof(Platform), model.Platform)
                }, "test" + DateTime.UtcNow.Ticks, model.DeviceIds);

                if (registrationResponse.IsSuccess == false) return BadRequest(registrationResponse.Message);
                return Ok(registrationResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route(RouteConstants.API.Notification.UnregisterDevice)]
        public async Task<IHttpActionResult> DeleteDeviceAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest();
                var deleteResponse = await PushNotificationFacade.Instance.Service.UnregisterDeviceAsync(id);
                if (deleteResponse.IsSuccess == false) return BadRequest(deleteResponse.Message);

                return Ok(deleteResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route(RouteConstants.API.Notification.TestNotification)]
        public async Task<IHttpActionResult> TestNotificationAsync(int deviceId)
        {
            try
            {
                var message = new InformationDeviceMessage
                {
                    DeviceId = deviceId,
                    Direction = MovingDirection.DownOrExit,
                    Door = DoorStatus.Open,
                    Location = "test location",
                    Other = "other",
                    Status = Status.Alert,
                    Temperature = 22
                };

                var response = await PushNotificationFacade.Instance.Service.SendNotificationAsync(deviceId, message);
                if (response.IsSuccess == false) return BadRequest(response.Message);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
