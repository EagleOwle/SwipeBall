using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip pic;
    [SerializeField] private Push push;
    [SerializeField] private SleepCalculate sleepCalculate;

    private bool onTarget = false;

    public void Initialise(Follow follow, IChangeGameSate changeGameSate)
    {
        follow.actionSetTarget += CameraChangeFollow;
        follow.DefaultTarget = transform;
        follow.Target = transform;
        onTarget = true;

        changeGameSate.ChangeGameSate += ChangeGameSate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onTarget) return;

        if (other.TryGetComponent(out Item coin))
        {
            coin.OnHit();
            PlaySoundEffect(pic);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void CameraChangeFollow(Transform value)
    {
        if(value != this.transform)
        {
            onTarget = false;
            push.SelfDisable();
            sleepCalculate.SelfDisable();
        }
        else
        {
            onTarget = true;
            push.SelfEnable();
            sleepCalculate.SelfEnable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude < 1) return;

        Vector3 normal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.LookRotation(normal);
        Pool.Instance.SpawnPooledObject(PoolElementType.Hit, transform.position, rotation);
        PlaySoundEffect(hit);
    }

    private void ChangeGameSate(GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                push.SelfEnable();
                sleepCalculate.SelfEnable();
                break;
            case GameState.Pause:
                push.SelfDisable();
                sleepCalculate.SelfDisable();
                break;
        }
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        SoundController.Instance.PlayClipAtPosition(clip, transform.position);
    }

}
