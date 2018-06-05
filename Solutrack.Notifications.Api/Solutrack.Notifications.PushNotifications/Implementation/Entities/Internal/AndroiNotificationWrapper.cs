using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Implementation.Entities.Internal
{
    internal class AndroidNotificationWrapper<T>
    {
        [JsonProperty(PropertyName = "data")]
        public AndroidNotificationData<T> Data { get; set; }
    }

    internal class AndroidNotificationData<T>
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "args")]
        public T Args { get; set; }
    }
}
