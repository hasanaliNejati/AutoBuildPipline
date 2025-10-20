using System.Collections.Generic;
using System.Linq;
using Common;
using BuildType = Common.BuildTypeSO.BuildType;
using UnityEditor;
using UnityEditor.Build;


public class DefineSymbolsHelper
{
    private static readonly Dictionary<BuildType, string> MarketSymbols =
        new Dictionary<BuildType, string>
        {
            { BuildType.Bazzar, "BAZAAR_STORE" },
            { BuildType.Myket, "MYKET_STORE" },
            { BuildType.GooglePlay, "GOOGLEPLAY_STORE" },
            { BuildType.Appstore, "APPSTORE_STORE" },
            { BuildType.Test, "TEST_BUILD" }
        };

    public static void SetSymbolsForMarket(NamedBuildTarget target, BuildType market)
    {
        string currentDefines = PlayerSettings.GetScriptingDefineSymbols(target);

        foreach (var symbol in MarketSymbols.Values)
            currentDefines = currentDefines.Replace(symbol, "");

        currentDefines = string.Join(";", currentDefines.Split(';')
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrEmpty(s)));

        if (MarketSymbols.TryGetValue(market, out var symbolToAdd))
        {
            if (!string.IsNullOrEmpty(currentDefines))
                currentDefines += ";";
            currentDefines += symbolToAdd;
        }

        PlayerSettings.SetScriptingDefineSymbols(target, currentDefines);
        
    }
}