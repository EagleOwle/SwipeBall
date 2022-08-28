using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public void OnHit()
    {
        if (Pool.Instance != null)
        {
            Pool.Instance.SpawnPooledObject(PoolElementType.Ballon, transform.position + Vector3.up * 2, Quaternion.identity);
            Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ObjectPool.Instance is null");
        }

        actionOnHit.Invoke(this);
        Destroy(gameObject);
    }

}
