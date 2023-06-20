using System.Collections.Generic;
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
                _instance = Resources.Load("PrefabsStore") as PrefabsStore;

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

    public PoolElement GetPoolingPrefabOfType<T>() where T : PoolElement
    {
        foreach (var item in poolingPrefabs)
        {
            if(item is T)
            {
                return item;
            }
        }

        Debug.LogWarning("No Prefab of type: " + typeof(T).ToString());
        return null;
    }

    public Item[] ItemPrefabs => itemPrefabs;
    [SerializeField] private Item[] itemPrefabs;

    public Present[] PresentPrefabs => presentPrefabs;
    [SerializeField] private Present[] presentPrefabs;
    
    public Present RandomPresent
    {
        get
        {
            int rnd = Random.Range(0, presentPrefabs.Length);
            Present present = presentPrefabs[rnd];
            return present;
        }
    }

    public List<PresentPreference> balls;

    public Ball GetPrefabBallByName(string ballName)
    {
        foreach (var item in balls)
        {
            if (item.name == ballName)
            {
                return item.ballPrefab;
            }
        }

        Debug.LogError("No Ball Preference by Name");
        return null;
    }

    public void SetCurrentBall(int index)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (i == index)
            {
                balls[i].current = true;
            }
            else
            {
                balls[i].current = false;
            }
        }
    }

    public Ball CurrentBallPrefab
    {
        get
        {
            foreach (var item in balls)
            {
                if (item.current == true)
                {
                    return item.ballPrefab;
                }
            }

            Debug.LogError("No current ball in Present Preference");
            return null;
        }
    }

}
