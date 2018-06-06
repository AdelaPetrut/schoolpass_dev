using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutrack.Notifications.Api.Models
{
    public class DeviceRegistrationModel
    {
        public string Platform { get; set; }
        public string Handle { get; set; }
        public List<int> DeviceIds { get; set; }
    }
}