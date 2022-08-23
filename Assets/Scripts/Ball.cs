using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask cointMask;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip pic;

    private void Start()
    {
        EventSpace.SetFollowTarget.AddListener(CameraChangeFollow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & cointMask) != 0)
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pic);
        }
    }

    private void CameraChangeFollow(Transform value)
    {
        if(value != this.transform)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(hit);
    }
}
