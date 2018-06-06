using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class InformationDeviceMessage : BaseDeviceMessage, IDeviceMessage
    {
        public string Location { get; set; }

        public Enums.MovingDirection Direction { get; set; }

        public Enums.DoorStatus Door { get; set; }

        public string Other { get; set; }

        //Described in Celsius
        public int Temperature { get; set; }
    }
}
