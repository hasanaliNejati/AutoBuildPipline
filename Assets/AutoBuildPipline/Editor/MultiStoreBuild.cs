using UnityEditor.Build;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;
using static Common.BuildTypeSO.BuildType; // برای استفاده راحت از enum (مثلا Bazzar, Myket)

public static class MultiStoreBuild
{
    public enum BuildArchitecture
    {
        AllInOne,
        Seperated,
        V7,
        Arm46
    }

    private const string outputPath = "Builds/Android";
    private const string appName = "App";


    public static void BuildForMarket(Common.BuildTypeSO.BuildType market, BuildArchitecture architecture)
    {
        DefineSymbolsHelper.SetSymbolsForMarket(NamedBuildTarget.Android, market);
        int lastBundleVersion = PlayerSettings.Android.bundleVersionCode;

        PlayerSettings.Android.keystorePass = "13811381";
        PlayerSettings.Android.keyaliasPass = "13811381";

        string storeName = market.ToString();
        


        if (architecture == BuildArchitecture.AllInOne)
        {
            GradleManager.ApplyGradleTemplates(market);
            string allInOnePath = Path.Combine(outputPath,
                $"{appName}_{storeName}_" + architecture + "_" + PlayerSettings.Android.bundleVersionCode + ".apk");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, allInOnePath, BuildTarget.Android,
                BuildOptions.None);
            Debug.Log("ARMv7 build done: " + report.summary.result);
        }

        if (architecture == BuildArchitecture.Seperated || architecture == BuildArchitecture.V7)
        {
            GradleManager.ApplyGradleTemplates(market);
            string pathArmv7 = Path.Combine(outputPath,
                $"{appName}_{storeName}_armv7_" + PlayerSettings.Android.bundleVersionCode + ".apk");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            BuildReport report1 = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, pathArmv7, BuildTarget.Android,
                BuildOptions.None);
            Debug.Log("ARMv7 build done: " + report1.summary.result);
        }

        if (architecture == BuildArchitecture.Seperated)
        {
            PlayerSettings.Android.bundleVersionCode++;
            Debug.Log("New versionCode: " + PlayerSettings.Android.bundleVersionCode);
        }

        if (architecture == BuildArchitecture.Seperated || architecture == BuildArchitecture.Arm46)
        {
            GradleManager.ApplyGradleTemplates(market);
            string pathArm64 = Path.Combine(outputPath,
                $"{appName}_{storeName}_arm64_" + PlayerSettings.Android.bundleVersionCode + ".apk");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            BuildReport report2 = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, pathArm64, BuildTarget.Android,
                BuildOptions.None);
            Debug.Log("ARM64 build done: " + report2.summary.result);
        }

        PlayerSettings.Android.bundleVersionCode = lastBundleVersion;
    }


}