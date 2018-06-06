using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class AcknowledgmentDeviceMessage : BaseDeviceMessage, IDeviceMessage
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventCode { get; set; }
        public string EventDescription { get; set; }
    }
}
