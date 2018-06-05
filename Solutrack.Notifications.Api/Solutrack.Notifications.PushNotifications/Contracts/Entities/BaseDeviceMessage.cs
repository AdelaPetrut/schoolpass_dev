using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class BaseDeviceMessage
    {
        public int DeviceId { get; set; }

        public DeviceEnums.Status Status { get; set; }
    }
}
