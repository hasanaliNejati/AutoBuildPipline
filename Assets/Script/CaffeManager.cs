#if BAZAAR_STORE

using System;
using Bazaar.Data;
using Bazaar.Poolakey;
using Bazaar.Poolakey.Data;
using Common.IAP;
using Zenject;

namespace Bazaar
{
    public class CaffeManager : IIAPManager
    {
        private Payment payment;
        private bool connected;
        [Inject] 
        public void Cunstructor()
        {
            
            
            SecurityCheck securityCheck = SecurityCheck.Disable();
            PaymentConfiguration paymentConfiguration = new PaymentConfiguration(securityCheck);
            payment = new Payment(paymentConfiguration);
            
            payment.Connect((r) =>
            {
                if (r.data)
                {
                    connected = true;
                }
                else
                {
                    
                }
            });
        }
        
        public void Purchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            if (!connected)
            {
                payment.Connect((r) =>
                {
                    if (r.data)
                    {
                        connected = true;
                        
                        DoPurchase(productId, onSuccess, onError);
                    }
                    else
                    {
                        onError?.Invoke(r.message);
                    }
                });
            }else
            {
                DoPurchase(productId, onSuccess, onError);
            }
            
        }

        public void Cunsume(string productId,string productToken, Action<string> onSuccess, Action<string> onError)
        {
            _ = payment.Consume(productToken, OnConsumeComlete);

            void OnConsumeComlete(Result<bool> result)
            {
                if (result.data)
                {
                    onSuccess.Invoke(productToken);
                }
                else
                {
                    onError.Invoke(result.message);
                }
            }
        }

        void DoPurchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            _ = payment.Purchase(productId, SKUDetails.Type.inApp, OnPuschaseStart, OnPuschaseComplete, productId);

            void OnPuschaseStart(Result<PurchaseInfo> result)
            {
                
            }

            void OnPuschaseComplete(Result<PurchaseInfo> result)
            {
                if (result.status == Status.Success)
                {
                    onSuccess?.Invoke(result.data.productId,result.data.purchaseToken);
                }
                else
                {
                    onError?.Invoke(result.message);
                    if (result.status == Status.Disconnected)
                    {
                        connected = false;
                    }
                }
            }
        }
    }
}

#endif