#if MYKET_STORE
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Common.IAP
{
    

    
    using MyketPlugin;
    
    public class MyketManagerObject : MonoBehaviour
    {
        
        

        public static MyketManagerObject instance;
        
        private bool connected;
        Dictionary<string, Request> requests = new Dictionary<string, Request>();
        Dictionary<string, RequestCunsume> consumeRequests = new Dictionary<string, RequestCunsume>();
        
        public void Start()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            instance = this;
            IABEventManager.billingSupportedEvent += IABEventManager_billingSupportedEvent;
            IABEventManager.billingNotSupportedEvent += IABEventManager_billingNotSupportedEvent;
            IABEventManager.purchaseSucceededEvent += IABEventManager_purchaseSucceededEvent;
            IABEventManager.purchaseFailedEvent += IABEventManager_purchaseFailedEvent;
            IABEventManager.consumePurchaseSucceededEvent += IABEventManager_consumeSucceededEvent;
            IABEventManager.consumePurchaseFailedEvent += IABEventManager_cunsumeFailedEvent;
            
            Init();
            

        }


        public void Init()
        {
            MyketIAB.init("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCuow60+P9dN0zG1YygVPliGSOtV7rtdQlIQLVYGZelu3qyHqLrKHhHk9QjstsMpcjnJDArcripmOuOVdaN3nsIfZUk5KVk8cgBxYIL9Bsyg2SQobdKZrWklC/0zJCtx0WzepeG4Bx36rWFBKWF+3amH8FhSD08pi7rtX81pRiuCQIDAQAB");   
        }
        
        
        public void Purchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            Debug.Log("purchase");
            if (!connected)
            {
                Init();
                onError.Invoke("initialize failed");
            }else
            {
                DoPurchase(productId, onSuccess, onError);
            }
            
        }

        public void Cunsume(string productToken, Action<string> onSuccess, Action<string> onError)
        {
            consumeRequests.Add(productToken,new RequestCunsume(onSuccess,onError));
            MyketIAB.consumeProduct(productToken);
            
            // _ = payment.Consume(productToken, OnConsumeComlete);
            //
            // void OnConsumeComlete(Result<bool> result)
            // {
            //     if (result.data)
            //     {
            //         onSuccess.Invoke(productToken);
            //     }
            //     else
            //     {
            //         onError.Invoke(result.message);
            //     }
            // }
        }
        
        
        private void IABEventManager_purchaseFailedEvent(string obj)
        {
            Debug.Log(obj);
            if (requests.Count >= 0)
            {
                string c_Key = requests.Last().Key;
                requests[c_Key].onError.Invoke(obj);
                requests.Remove(c_Key);  

            }
        }

        private void IABEventManager_purchaseSucceededEvent(MyketPurchase obj)
        {
            Debug.Log("2");
            
            if (requests.ContainsKey(obj.ProductId))
            {
                Debug.Log("3");
                requests[obj.ProductId].onSuccess.Invoke(obj.ProductId,obj.PurchaseToken);
                
                requests.Remove(obj.ProductId);
            }

            // var item = FindObjectsByType<MiniNoticeController>(FindObjectsInactive.Include, FindObjectsSortMode.None)[0];
            // item.ShowNotice(obj.ProductId,MiniNoticeType.Info);
            // item.ShowNotice(obj.PurchaseToken,MiniNoticeType.Info);
        }

        private void IABEventManager_billingNotSupportedEvent(string obj)
        {
            connected = false;

        }

        private void IABEventManager_billingSupportedEvent()
        {
            connected = true;
            // errorText.text = initialized.ToString();
            Debug.Log("okkkkkkkkkkkkkk");
        }

        private void IABEventManager_cunsumeFailedEvent(string error)
        {
            if (consumeRequests.Count >= 0)
            {
                string c_Key = consumeRequests.Last().Key;
                consumeRequests[c_Key].onError.Invoke(error);
                consumeRequests.Remove(c_Key);  

            }
        }

        private void IABEventManager_consumeSucceededEvent(MyketPurchase myketPurchase)
        {

            
            if (consumeRequests.ContainsKey(myketPurchase.ProductId))
            {
                consumeRequests[myketPurchase.ProductId].onSuccess.Invoke(myketPurchase.PurchaseToken);
                
                consumeRequests.Remove(myketPurchase.ProductId);
            }
        }
        
        void DoPurchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            if (requests.ContainsKey(productId))
                requests[productId] = new Request(onSuccess, onError);
            else
                requests.Add(productId, new Request(onSuccess, onError));
            Debug.Log("11111111111");
            MyketIAB.purchaseProduct(productId);
            
           
        }
    }


}

#endif