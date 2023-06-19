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

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultMeshScale;

    private void Awake()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation;
        defaultMeshScale = meshTransform.localScale;
    }

    public void Initialise()
    {
        gameObject.SetActive(true);
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
            Pool.Instance.SpawnPooledObject(PoolElementType.Rain, transform.position, Quaternion.identity);

            //BallPresent presentPrefab = PrefabsStore.Instance.balls[0].present;
            //Instantiate(presentPrefab, transform.position, Quaternion.identity);
        }

        CancelInvoke();
        EventSpace.ScreenRayHitCollider.RemoveListener(HitCollider);
        PlaySoundEffect(pop);
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

    private void PlaySoundEffect(AudioClip clip)
    {
        SoundController.Instance.PlayClipAtPosition(clip, transform.position);
    }

    public class EventBallonOnTouch : UnityEvent<Balloon>
    {

    }
}
