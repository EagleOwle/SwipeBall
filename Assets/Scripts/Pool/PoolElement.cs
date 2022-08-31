using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolElement : MonoBehaviour
{
    public PoolElementType type;

    public virtual void Instantiate()
    {

    }

    public void ToPool()
    {
        Pool.Instance.ReturnToPool(this);
    }
}
