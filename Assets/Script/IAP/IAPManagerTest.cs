using System;

namespace Common.IAP
{
    public class IAPManagerTest : IIAPManager
    {
        public void Purchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            // Simulate a successful purchase
            onSuccess?.Invoke(productId,"testToken");

            // Uncomment the following line to simulate an error
            // onError?.Invoke("Purchase failed");
        }

        public void Cunsume(string productId,string token, Action<string> onSuccess, Action<string> onError)
        {
            //onSuccess.Invoke(productId);
            onSuccess.Invoke("testToken");
        }
    }
}