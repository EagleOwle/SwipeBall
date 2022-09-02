using UnityEngine;
using System.Collections;
using System;

public abstract class Item : MonoBehaviour
{
    public Action<Item> actionOnHit;

    public virtual void OnHit()
    {

    }
}
