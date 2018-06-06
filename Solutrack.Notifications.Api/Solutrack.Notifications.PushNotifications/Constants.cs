using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications
{
    internal static class Constants
    {
        public const string NOTIFICATION_MESSAGE_KEY = "message";
        public const string BASE64_MESSAGE_KEY = "encodedMessage";
        public const string MESSAGE_TYPE = "messageType";

        public const string NOTIFICATION_NAME = "Solutrack";

        public const string GENERIC_ERROR_MESSAGE = "Something went wrong. For more information please check the following stack trace: ";
        public const string GENERIC_ERROR_REGISTER_DEVICE = "Something went wrong during the registration process.";
        public const string INVALID_MESSSAGE = "Invalid Message";
        public const string INVALID_DEVICE_ID = "Invalid device id";


        public const string NOTIFICATION_ABANDONED= "Notification was abandoned";
        public const string NOTIFICATION_CANCELLED = "Notification was cancelled";
        public const string NOTIFICATION_NO_TARGET_FOUND = "No target found for this notification";
        public const string NOTIFICATION_SENT_OR_IS_PROCESSED = "Notification was delivered or is currently processed";
    }
}
