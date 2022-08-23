using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnDestroy()
    {
        if (Pool.Instance != null)
        {
            Pool.Instance.SpawnPooledObject(PoolElementType.Ballon, transform.position, Quaternion.identity);
            Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ObjectPool.Instance is null");
        }
    }

}
