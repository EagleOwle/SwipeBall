using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolElement : MonoBehaviour
{
    public virtual void FromPool()
    {

    }

    public void ToPool()
    {
        Pool.Instance.ReturnToPool(this);
    }

}
