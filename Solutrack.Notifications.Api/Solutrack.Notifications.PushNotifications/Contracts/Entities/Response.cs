using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutrack.Notifications.PushNotifications.Contracts.Entities
{
    public class Response
    {
        protected bool _isActionSuccess;

        public Response()
        {
            this._isActionSuccess = true;
        }

        public Response(bool isSuccessful)
        {
            this._isActionSuccess = isSuccessful;
        }

        public Response(bool isSuccessful, string message)
        {
            this._isActionSuccess = isSuccessful;
            this.Message = message;
        }

        public string Message
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get
            {
                return this._isActionSuccess;
            }
            set
            {
                this._isActionSuccess = value;
            }
        }
    }

    public class Response<T>
    {
        protected bool _isActionSuccess;

        public Response()
        {
            this._isActionSuccess = true;
        }

        public Response(T value)
        {
            this._isActionSuccess = true;
            this.Value = value;
        }

        public Response(bool isSuccessful)
        {
            this._isActionSuccess = isSuccessful;
        }

      
        public Response(bool isSuccessful, string message)
        {
            this._isActionSuccess = isSuccessful;
            this.Message = message;
        }

      
        public Response(bool isSuccess, string message, T value)
        {
            this._isActionSuccess = isSuccess;
            this.Message = message;
            this.Value = value;
        }

     
        public string Message
        {
            get;
            set;
        }


        public bool IsSuccess
        {
            get
            {
                return this._isActionSuccess;
            }
            set
            {
                this._isActionSuccess = value;
            }
        }

        public T Value
        {
            get;
            set;
        }
    }
}
