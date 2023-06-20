using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    public Item SpawnItem()
    {
        return Instantiate(PrefabsStore.Instance.ItemPrefabs[1], transform);
    }
}
