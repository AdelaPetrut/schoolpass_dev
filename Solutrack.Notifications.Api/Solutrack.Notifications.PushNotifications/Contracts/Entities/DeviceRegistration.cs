using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solutrack.Notifications.PushNotifications.Contracts.Enums;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class DeviceRegistration
    {
        public Platform Platform { get; set; }
        public string DeviceToken { get; set; }
    }
}
