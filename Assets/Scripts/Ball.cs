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
    [SerializeField] private Push push;

    private bool onTarget = false; 

    private void Start()
    {
        EventSpace.SetFollowTarget.AddListener(CameraChangeFollow);
        onTarget = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onTarget) return;

        //if ((1 << other.gameObject.layer & cointMask) != 0)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                coin.OnHit();
                audioSource.PlayOneShot(pic);
            }
        }
    }

    private void CameraChangeFollow(Transform value)
    {
        if(value != this.transform)
        {
            onTarget = false;
            //rigidbody.velocity = Vector3.zero;
            //rigidbody.angularVelocity = Vector3.zero;
            push.Enable(false);
        }
        else
        {
            onTarget = true;
            push.Enable(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(hit);
    }
}
