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
    public PoolElement[] PoolingPrefabs => poolingPrefabs;

    public PoolElement GetPoolingPrefabOfType(PoolElementType type)
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

    [SerializeField] private Item[] itemPrefabs;
    public Item[] ItemPrefabs => itemPrefabs;

    [SerializeField]private Ball ballPrefab;
    public Ball BallPrefab => ballPrefab;

}
