using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override void OnHit()
    {
        if (Pool.Instance  == null)
        {
            Debug.LogWarning("ObjectPool.Instance is null");
            return;
        }

        //Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        Pool.Instance.SpawnPooledObject(PoolElementType.Hit, transform.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Pool.Instance.SpawnPooledObject(PoolElementType.Simple, transform.position, rotation);
        Pool.Instance.SpawnPooledObject(PoolElementType.Ballon, transform.position + Vector3.up * 2, Quaternion.identity);

        actionOnHit.Invoke(this);
        Destroy(gameObject);
    }

}
