#if MYKET_STORE
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Zenject;

namespace Common.IAP
{

    public class IAPManagerMyket : IIAPManager
    {
        public void Purchase(string productId, Action<string,string> onSuccess, Action<string> onError)
        {
            Debug.Log("pppp");
            MyketManagerObject.instance.Purchase(productId,onSuccess,onError);
        }

        public void Cunsume(string productId,string token, Action<string> onSuccess, Action<string> onError)
        {
            MyketManagerObject.instance.Cunsume(productId, onSuccess, onError);
        }
    }


}

#endif