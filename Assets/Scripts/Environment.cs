using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Environment : MonoBehaviour
{
    public Action<int> actionChangeItemCount;

    [SerializeField] private SpawnPoint[] spawnPoints;
    private List<Item> items = new List<Item>();

    //private void OnValidate()
    //{
    //    spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
    //}

    public void Initialise()
    {
        foreach (var item in spawnPoints)
        {
            var tmp = Instantiate(PrefabsStore.Instance.ItemPrefabs[0], item.transform.position, Quaternion.identity);
            tmp.actionOnHit += RemoveItem;
            items.Add(tmp);
        }
    }

    private void RemoveItem(Item item)
    {
        items.Remove(item);
        actionChangeItemCount.Invoke(items.Count);
    }


}
