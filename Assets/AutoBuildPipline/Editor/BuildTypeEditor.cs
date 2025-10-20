
using UnityEditor;
using UnityEngine;
using Common;
using UnityEditor.Build;

[CustomEditor(typeof(BuildTypeSO))]
public class BuildTypeSOEditor : Editor
{

    public override void OnInspectorGUI()
    {
        BuildTypeSO buildTypeSO = (BuildTypeSO)target;

        var lastBuildType = buildTypeSO.buildType;
        buildTypeSO.buildType =
            (BuildTypeSO.BuildType)EditorGUILayout.EnumPopup("Build Type", buildTypeSO.buildType);

        buildTypeSO.testMarketBuild = EditorGUILayout.Toggle("Is Test Market Build", buildTypeSO.testMarketBuild);
        if (GUILayout.Button("Apply"))
        {
            ApplyBuildType();
        }
        if (GUILayout.Button("Apply Gradle"))
        {
            GradleManager.ApplyGradleTemplates(buildTypeSO.buildType);
        }
        if (GUILayout.Button("V7 Build for Selected Market"))
        {
            MultiStoreBuild.BuildForMarket(buildTypeSO.GetBuildType(),MultiStoreBuild.BuildArchitecture.V7);
        }
        if (GUILayout.Button("AllInOne Build for Selected Market"))
        {
            MultiStoreBuild.BuildForMarket(buildTypeSO.GetBuildType(),MultiStoreBuild.BuildArchitecture.AllInOne);
        }
        if (GUILayout.Button("Seperated Build for Selected Market"))
        {
            MultiStoreBuild.BuildForMarket(buildTypeSO.GetBuildType(),MultiStoreBuild.BuildArchitecture.Seperated);
        }
        if (GUILayout.Button("Seperated Build for Both Market"))
        {
            buildTypeSO.buildType = BuildTypeSO.BuildType.Bazzar;
            MultiStoreBuild.BuildForMarket(buildTypeSO.GetBuildType(),MultiStoreBuild.BuildArchitecture.Seperated);
            buildTypeSO.buildType = BuildTypeSO.BuildType.Myket;
            MultiStoreBuild.BuildForMarket(buildTypeSO.GetBuildType(),MultiStoreBuild.BuildArchitecture.Seperated);
        }

    }

    private void ApplyBuildType()
    {
        BuildTypeSO buildTypeSO = (BuildTypeSO)target;

        DefineSymbolsHelper.SetSymbolsForMarket(NamedBuildTarget.Android, buildTypeSO.GetBuildType());
        Debug.Log($"✅ Applied define symbols for {buildTypeSO.GetBuildType()}");
    }
}
