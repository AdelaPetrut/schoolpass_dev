using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities.Internal
{
    internal class IOSNotificationWrapper<T>
    {
        [JsonProperty(PropertyName = "aps")]
        public IOSNotificationData Aps { get; set; }

        [JsonProperty(PropertyName = "args")]
        public T Args { get; set; }
    }

    internal class IOSNotificationData
    {
        [JsonProperty(PropertyName = "alert")]
        public string Alert { get; set; }

    }
}
