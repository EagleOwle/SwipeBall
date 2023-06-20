using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public event Action<int> EventChangeItemCount;

    private List<Item> items = new List<Item>();

    public void GenerateItem()
    {
        items = new List<Item>();

        foreach (var item in GameObject.FindObjectsOfType<ItemPoint>())
        {
            SpawnItems(item);
        }
    }

    private void SpawnItems(ItemPoint point)
    {
        Item item = point.SpawnItem();
        item.actionOnHit += RemoveItem;
        items.Add(item);
    }

    public int CurrentItemCount()
    {
       return items.Count;
    }

    private void RemoveItem(Item item)
    {
        items.Remove(item);
        EventChangeItemCount?.Invoke(items.Count);
    }


}