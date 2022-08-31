using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    #region Singleton
    private static Pool _instance;
    public static Pool Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Pool>();
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private int amountToPool = 5;

    private List<PoolElement> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<PoolElement>();

        foreach (var item in PrefabsStore.Instance.PoolingPrefabs)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                InstantiateNewObject(item.type);
            }
        }
    }

    public PoolElement GetPooledObject(PoolElementType type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] == null)
            {
                pooledObjects.RemoveAt(i);
                break;
            }

            if (pooledObjects[i].type == type)
            {
                if (pooledObjects[i].gameObject.activeInHierarchy == false)
                {
                    return pooledObjects[i];
                }
            }
        }

        PoolElement tmp = InstantiateNewObject(type);
        return tmp;
    }

    public GameObject SpawnPooledObject(PoolElementType type, Vector3 position, Quaternion rotation)
    {
        PoolElement tmp = GetPooledObject(type);
        
        tmp.transform.parent = null;
        tmp.transform.position = position;
        tmp.transform.rotation = rotation;
        tmp.gameObject.SetActive(true);
        tmp.Instantiate();

        return tmp.gameObject;
    }

    public void ReturnToPool(PoolElement instance)
    {
        instance.transform.SetParent(transform);
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;
        instance.gameObject.SetActive(false);
    }
    
    private PoolElement InstantiateNewObject(PoolElementType type)
    {
        PoolElement prefab = PrefabsStore.Instance.GetPoolingPrefabOfType(type);
        PoolElement tmp = Instantiate(prefab);
        pooledObjects.Add(tmp);
        ReturnToPool(tmp);
        
        return tmp;
    }

}
