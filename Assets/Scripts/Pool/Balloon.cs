using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Balloon : MonoBehaviour
{
    [SerializeField] private float upForce = 0.1f;
    [SerializeField] private float resizeSpeed = 1;
    [SerializeField] private AudioClip pop;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private AudioSource audioSource;

    public EventOnTouch eventOnTouch = new EventOnTouch();

    public void Initialise()
    {
        transform.localScale = Vector3.one * 0.1f;
        StartCoroutine(SetSize());
    }

    public void Initialise(float liveTime)
    {
        Initialise();
        Invoke(nameof(BalloonOnTouch), liveTime);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = Vector3.up * upForce;
    }

    private IEnumerator SetSize()
    {
        bool resize = true;

        while (resize)
        {
            yield return null;
            resize = false;
            if (transform.localScale.magnitude < Vector3.one.magnitude)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, resizeSpeed * Time.deltaTime);
                resize = true;
            }
        }
    }

    private void OnMouseDown()
    {
        BalloonOnTouch();
    }

    public void BalloonOnTouch()
    {
        if (Pool.Instance != null)
        {
            
            Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        }

        CancelInvoke();
        audioSource.PlayOneShot(pop);
        eventOnTouch.Invoke(this);
    }
}
