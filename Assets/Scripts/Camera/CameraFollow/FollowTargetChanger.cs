using System;
using System.Collections;
using UnityEngine;

public class FollowTargetChanger : MonoBehaviour
{
    public event Action<Transform> EventFollowSetTarget;
    [SerializeField] private Follow follow;

    private Transform defaultTarget;
    

    public void SetTarget(Transform target)
    {
        if(target == null)
        {
            target = defaultTarget;
        }

        follow.Target = target;
        EventFollowSetTarget?.Invoke(target);
    }

    public void SetDefaultTarget(Transform target)
    {
        defaultTarget = target;
    }

}