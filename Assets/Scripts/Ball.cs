using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip pic;
    [SerializeField] private Push push;
    [SerializeField] private SleepCalculate sleepCalculate;
    [SerializeField] private TutorialMenu tutorialMenu;

    private bool onTarget = false; 

    public void Initialise(Follow follow)
    {
        follow.actionSetTarget += CameraChangeFollow;
        follow.DefaultTarget = transform;
        follow.Target = transform;
        onTarget = true;

        sleepCalculate.eventIsSleep += IsSleep;
        tutorialMenu.Hide();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onTarget) return;

        if (other.TryGetComponent(out Item coin))
        {
            coin.OnHit();
            audioSource.PlayOneShot(pic);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void CameraChangeFollow(Transform value)
    {
        if(value != this.transform)
        {
            onTarget = false;
            OnPushable = false;
        }
        else
        {
            onTarget = true;
            OnPushable = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pool.Instance.SpawnPooledObject(PoolElementType.Hit, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(hit);
    }

    public bool OnPushable
    {
        set
        {
            push.Enable(value);
            sleepCalculate.Enable(value);
        }
    }

    private void IsSleep()
    {
        tutorialMenu.Show();
    }

}
