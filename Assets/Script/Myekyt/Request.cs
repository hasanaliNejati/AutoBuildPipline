using System;

namespace Common.IAP
{
    public class Request
    {
        public Request(Action<string,string> onSuccess, Action<string> onError)
        {
            this.onSuccess = onSuccess;
            this.onError = onError;
        }
        public Action<string,string> onSuccess;
        public Action<string> onError;
    }
    
    public class RequestCunsume
    {
        public RequestCunsume(Action<string> onSuccess, Action<string> onError)
        {
            this.onSuccess = onSuccess;
            this.onError = onError;
        }
        public Action<string> onSuccess;
        public Action<string> onError;
    }
}