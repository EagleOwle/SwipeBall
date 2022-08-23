using UnityEngine;

public class PrefabsStore : ScriptableObject
{
    #region Singleton
    private static PrefabsStore _instance;
    public static PrefabsStore Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load("PrefabsStore", typeof(PrefabsStore)) as PrefabsStore;

                if(_instance == null)
                {
                    Debug.LogError("_instance is null");
                }

            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private PoolElement[] poolingPrefabs;
    public PoolElement[] Prefabs => poolingPrefabs;

    public PoolElement GetPrefabOfType(PoolElementType type)
    {
        foreach (var item in poolingPrefabs)
        {
            if(item.type == type)
            {
                return item;
            }
        }

        Debug.LogWarning("No Prefab of type:" + type);
        return null;
    }

}
