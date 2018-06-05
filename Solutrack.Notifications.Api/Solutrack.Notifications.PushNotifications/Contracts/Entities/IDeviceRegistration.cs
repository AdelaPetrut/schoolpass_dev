using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solutrack.Notifications.PushNotifications.Implementation.Entities.Enums;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public interface IDeviceRegistration
    {
        Platform Platform { get; set; }
        string DeviceToken { get; set; }
        string[] Tags { get; set; }
    }
}
