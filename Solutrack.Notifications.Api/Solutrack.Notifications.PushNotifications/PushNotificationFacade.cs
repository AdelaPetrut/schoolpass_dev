using Microsoft.Azure.NotificationHubs;
using Solutrack.Notifications.PushNotifications.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications
{
    public class PushNotificationFacade
    {
        private string azureHubConnectionString;
        private string azureHubName;

        #region Singleton
        private static PushNotificationFacade instance;

        public static PushNotificationFacade Instance
        {
            get
            {
                if (instance == null) instance = new PushNotificationFacade();

                return instance;
            }
        }

        private PushNotificationFacade()
        {

        }
        #endregion

        #region Initialization

        public void Initialize(string azureHubConnectionString, string azureHubName)
        {
            this.azureHubName = azureHubName;
            this.azureHubConnectionString = azureHubConnectionString;
        }

        #endregion

        public PushNotificationsService Service
        {
            get
            {
                if (string.IsNullOrEmpty(azureHubName) || string.IsNullOrEmpty(azureHubConnectionString)) throw new Exception("Notifications Not initialized. Please call PushNotificationFacade.Instance.Initialize");
                return new PushNotificationsService(NotificationHubClient.CreateClientFromConnectionString(this.azureHubConnectionString, this.azureHubName));
            }
        }
    }
}
