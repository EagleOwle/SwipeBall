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
              //  InstantiateNewObject(item.type);
            }
        }
    }

    public PoolElement GetPooledObject<T>() where T : PoolElement
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] == null)
            {
                pooledObjects.RemoveAt(i);
                break;
            }

            if (pooledObjects[i] is T)
            {
                if (pooledObjects[i].gameObject.activeInHierarchy == false)
                {
                    return pooledObjects[i];
                }
            }
        }

        PoolElement tmp = InstantiateNewObject<T>();
        return tmp;
    }

    public PoolElement GetNewPooledObject<T>() where T : PoolElement
    {
        PoolElement tmp = InstantiateNewObject<T>();
        return tmp;
    }

    public GameObject SpawnPooledObject<T>(Vector3 position, Quaternion rotation) where T : PoolElement
    {
        PoolElement tmp = GetPooledObject<T>();
        
        tmp.transform.parent = null;
        tmp.transform.position = position;
        tmp.transform.rotation = rotation;
        tmp.gameObject.SetActive(true);
        tmp.FromPool();

        return tmp.gameObject;
    }

    public GameObject SpawnNewPooledObject<T>( Vector3 position, Quaternion rotation) where T : PoolElement
    {
        PoolElement tmp = GetNewPooledObject<T>();

        tmp.transform.parent = null;
        tmp.transform.position = position;
        tmp.transform.rotation = rotation;
        tmp.gameObject.SetActive(true);
        tmp.FromPool();

        return tmp.gameObject;
    }

    public void ReturnToPool(PoolElement instance)
    {
        instance.transform.SetParent(transform);
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;
        instance.gameObject.SetActive(false);
    }
    
    private PoolElement InstantiateNewObject<T>() where T : PoolElement
    {
        PoolElement prefab = PrefabsStore.Instance.GetPoolingPrefabOfType<T>();
        PoolElement tmp = Instantiate(prefab);
        pooledObjects.Add(tmp);
        ReturnToPool(tmp);
        
        return tmp;
    }

}
