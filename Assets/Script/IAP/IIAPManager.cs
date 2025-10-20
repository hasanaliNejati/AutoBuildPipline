using System;

namespace Common.IAP
{
    public interface IIAPManager
    {
        public void Purchase(string productId, Action<string,string> onSuccess, Action<string> onError);
        public void Cunsume(string productId,string token, Action<string> onSuccess, Action<string> onError);
    }
}