using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts
{
    public class DeviceEnums
    {
        public enum MovingDirection
        {
            //In case of Moving Walk equipment, Enter means going toward to the terminal/gate; Exit is the contrary
            Stationary = 10,
            UpOrEnter = 20,
            DownOrExit = 30
        }

        public enum DoorStatus
        {
            Open = 10,
            Close = 20
        }

        public enum Status
        {
            //Grey: Not communicating or no signal or no power
            Offline = 10,

            //Green: Running Working fine
            Running = 20,

            //Blue: General errors received
            Guarded = 30,

            //Yellow: Distress errors received
            Elevated = 40,

            //Orange: Severe errors received
            Alert = 50,

            //Red: Shutdown or Entrapment
            Emergency = 60
        }
    }
}
