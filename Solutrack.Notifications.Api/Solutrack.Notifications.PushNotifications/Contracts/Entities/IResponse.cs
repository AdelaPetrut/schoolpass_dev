using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public interface IResponse
    {
        string Message { get; set; }
        bool IsSuccess { get; set; }
    }

    public interface IResponse<T> : IResponse
    {
        T Value { get; set; }
    }
}
