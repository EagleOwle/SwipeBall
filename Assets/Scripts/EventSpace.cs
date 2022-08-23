using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public static class EventSpace
{
    public static EventSetTransform SetFollowTarget = new EventSetTransform();
}

public class EventSetTransform : UnityEvent<Transform>
{

}

