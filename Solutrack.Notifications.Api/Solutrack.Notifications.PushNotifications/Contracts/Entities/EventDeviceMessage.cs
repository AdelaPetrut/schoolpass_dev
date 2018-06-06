using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class EventDeviceMessage : BaseDeviceMessage, IDeviceMessage
    {
        public DateTime Date { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int ActivityId { get; set; }
    }
}
