using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Balloon : Item
{
    public EventBallonOnTouch eventOnTouch = new EventBallonOnTouch();

    [SerializeField] private AudioClip pop;
    [SerializeField] private MeshCollider balloonCollider;
    [SerializeField] private Transform meshTransform;

    private AudioSource audioSource;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultMeshScale;

    private void Awake()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation;
        defaultMeshScale = meshTransform.localScale;
    }

    public void Initialise(AudioSource audioSource)
    {
        gameObject.SetActive(true);
        this.audioSource = audioSource;
        EventSpace.ScreenRayHitCollider.AddListener(HitCollider);
    }

    public void Initialise(float liveTime, AudioSource audioSource)
    {
        Initialise(audioSource);
        Invoke(nameof(BalloonOnTouch), liveTime);
    }

    private void BalloonOnTouch()
    {
        if (Pool.Instance != null)
        {
            Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
            Pool.Instance.SpawnPooledObject(PoolElementType.Rain, transform.position, Quaternion.identity);
        }

        CancelInvoke();
        EventSpace.ScreenRayHitCollider.RemoveListener(HitCollider);
        audioSource.PlayOneShot(pop);
        audioSource.clip = pop;
        audioSource.loop = false;
        audioSource.Play();
        eventOnTouch.Invoke(this);
    }

    private void HitCollider(Collider collider)
    {
        if (collider == balloonCollider)
        {
            BalloonOnTouch();
        }
    }

    public void Disable()
    {
        meshTransform.localScale = defaultMeshScale;
        transform.localPosition = defaultPosition;
        transform.localRotation = defaultRotation;

        gameObject.SetActive(false);
    }

    public class EventBallonOnTouch : UnityEvent<Balloon>
    {

    }
}
