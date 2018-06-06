using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications
{
    public static class StringExtensions
    {
        public static string EncodeToBase64String(this string original)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(original));
        }

    }
}
