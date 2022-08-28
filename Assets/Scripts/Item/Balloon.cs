using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Balloon : Item
{
    public EventBallonOnTouch eventOnTouch = new EventBallonOnTouch();

    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Collider balloonCollider;

    public void Initialise()
    {
        EventSpace.ScreenRayHitCollider.AddListener(HitCollider);
    }

    public void Initialise(float liveTime)
    {
        Initialise();
        Invoke(nameof(BalloonOnTouch), liveTime);
    }

    private void BalloonOnTouch()
    {
        if (Pool.Instance != null)
        {
            Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        }

        CancelInvoke();
        EventSpace.ScreenRayHitCollider.RemoveListener(HitCollider);
        audioSource.PlayOneShot(pop);
        eventOnTouch.Invoke(this);
    }

    private void HitCollider(Collider collider)
    {
        if (collider == balloonCollider)
        {
            BalloonOnTouch();
        }
    }

    public class EventBallonOnTouch : UnityEvent<Balloon>
    {

    }
}
