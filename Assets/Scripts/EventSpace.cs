using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public static class EventSpace
{
    public static EventSetTransform SetFollowTarget = new EventSetTransform();
    public static EventSetCollider ScreenRayHitCollider = new EventSetCollider();
}

public class EventSetTransform : UnityEvent<Transform>
{

}

public class EventSetCollider : UnityEvent<Collider>
{

}

