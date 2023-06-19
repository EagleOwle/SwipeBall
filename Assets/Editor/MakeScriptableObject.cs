using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MakeScriptableObject
{
    [MenuItem("Assets/Create/Game Data/Create Prefabs Store")]
    public static void CreateMyAsset()
    {
        PrefabsStore asset = ScriptableObject.CreateInstance<PrefabsStore>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/PrefabsStore.asset");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Create/Game Data/Create PlayerData")]
    public static void CreatePlayerData()
    {
        PlayerData asset = ScriptableObject.CreateInstance<PlayerData>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/PlayerData.asset");
        AssetDatabase.SaveAssets();
    }
}
