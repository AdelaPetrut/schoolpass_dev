using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutrack.Notifications.Api
{
    public static class RouteConstants
    {
        public static class API
        {
            public static class Notification
            {
                public const string GetRegistrationId = @"get-registration-id";
                public const string RegisterDevice = @"register";
                public const string UnregisterDevice = @"unregister";

                public const string TestNotification = @"test";
            }
        }
    }
}