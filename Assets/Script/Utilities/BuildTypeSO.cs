using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    

    
    [CreateAssetMenu(fileName = "BuildTypeSO", menuName = "BuildType", order = 0)]
    public class BuildTypeSO : ScriptableObject
    {
        public enum BuildType
        {
            Test,
            Myket,
            Bazzar,
            GooglePlay,
            Appstore
        }

        [FormerlySerializedAs("marketType")] [SerializeField] public BuildType buildType;
        [SerializeField] public bool testMarketBuild;

        public BuildType GetBuildType()
        {
            return buildType;
        }

        public bool IsTest()
        {
            return testMarketBuild;
        }
    }
}