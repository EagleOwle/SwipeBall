using System.Collections;
using UnityEngine;

public struct SmoothFallowComponent
{
    public Transform selfTransform;
    public Transform targetTransform;
    public Vector3 offset;
    public float speed;
    public float maxDistance;
    public float currentDistance;
}