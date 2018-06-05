using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class BaseApplicationEvent
    {
        public string Username { get; set; }
        public string MobileAppToken { get; set; }
    }
}
