using Solutrack.Notifications.PushNotifications;
using Solutrack.Notifications.PushNotifications.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solutrack.Notifications.PushNotifications.Contracts.Enums;

namespace Solutrack.Notifications.ConsoleApp
{
    class Program
    {
        static string registrationId;

        static void Main(string[] args)
        {
            PushNotificationFacade.Instance.Initialize("Endpoint=sb://solutrack.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=SZNnZdpl0lITr/6EdopOLVPuCRdZVWa05FLYYbALOWs=", "solutrack");

            System.Console.WriteLine("Menu:");
            System.Console.WriteLine("1. Login");
            System.Console.WriteLine("2. Logout");
            System.Console.WriteLine("3. Send Notification");
            System.Console.WriteLine("4. Exit");


            string input = System.Console.ReadLine();
            while (input != "4")
            {
                switch (input)
                {
                    case ("1"):
                        {
                            System.Console.Write("Enter the device handle: ");
                            string handle = System.Console.ReadLine();
                            if (string.IsNullOrEmpty(handle)) return;

                            // get registrationId using handle

                            var registrationIdResponse = AsyncHelpers.RunSync<Response<string>>(() => { return PushNotificationFacade.Instance.Service.GetRegistrationIdForDeviceAsync(handle); });
                            if (registrationIdResponse.IsSuccess == false)
                            {
                                System.Console.WriteLine(registrationIdResponse.Message);
                                return;
                            }

                            // register device

                            var registrationResponse = AsyncHelpers.RunSync<Response>(() =>
                            {
                                return PushNotificationFacade.Instance.Service.RegisterDeviceAsync(registrationIdResponse.Value, new DeviceRegistration
                                {
                                    DeviceToken = handle,
                                    Platform = Platform.gcm
                                }, "synergo", new List<int> { 1, 2, 3 });

                            });
                            if (registrationResponse.IsSuccess == false)
                            {
                                System.Console.WriteLine(registrationResponse.Message);
                                return;
                            }
                            
                            registrationId = registrationIdResponse.Value;
                            System.Console.WriteLine("Your device is registered");
                            break;
                        }
                    case ("2"):
                        {

                            var unregisterResponse = AsyncHelpers.RunSync<Response>(() =>
                            {
                                return PushNotificationFacade.Instance.Service.UnregisterDeviceAsync(registrationId);
                            });
                            if (unregisterResponse.IsSuccess == false)
                            {
                                System.Console.WriteLine(unregisterResponse.Message);
                                return;
                            }
                            //registrationId = string.Empty;
                            System.Console.WriteLine("Your device is unregistered");
                            break;
                        }

                    case ("3"):
                        {
                            var message = new InformationDeviceMessage
                            {
                                DeviceId = 1,
                                Direction = MovingDirection.DownOrExit,
                                Door = DoorStatus.Open,
                                Location = "test location",
                                Other = "other",
                                Status = Status.Alert,
                                Temperature = 22
                            };

                            var sendResponse = AsyncHelpers.RunSync<Response>(() =>
                            {
                                return PushNotificationFacade.Instance.Service.SendNotificationAsync(1, message);
                            });
                            if (sendResponse.IsSuccess == false)
                            {
                                System.Console.WriteLine(sendResponse.Message);
                                return;
                            }
                            //registrationId = string.Empty;
                            System.Console.WriteLine("Notification sent");
                            break;
                        }
                }
                input = System.Console.ReadLine();
            }
        }
    }
}
