using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MakeScriptableObject
{
    [MenuItem("Assets/Create/Create Prefabs Store")]
    public static void CreateMyAsset()
    {
        PrefabsStore asset = ScriptableObject.CreateInstance<PrefabsStore>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/PrefabsStore.asset");
        AssetDatabase.SaveAssets();
    }
}
