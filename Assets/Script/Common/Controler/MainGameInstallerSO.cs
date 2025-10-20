using Common.IAP;
// using Common.Tapsell;
// using Nakama.Helpers;
using UnityEngine;
using Zenject;

namespace Common
{
    [CreateAssetMenu(fileName = "MainGameInstaller", menuName = "Zenject/MainGameInstaller", order = 0)]
    public class MainGameInstallerSO : ScriptableObjectInstaller<MainGameInstallerSO>
    {

        [SerializeField] private BuildTypeSO buildTypeSo;


        public override void InstallBindings()
        {
           

            
            
            

            
#if BAZAAR_STORE
            Container.Bind<IIAPManager>().To<Bazaar.CaffeManager>().FromNew().AsSingle().NonLazy();
#elif MYKET_STORE
            Container.Bind<IIAPManager>().To<IAPManagerMyket>().FromNew().AsSingle().NonLazy();
#else
            Container.Bind<IIAPManager>().To<IAPManagerTest>().FromNew().AsSingle().NonLazy();
#endif
            
        }
    }
}