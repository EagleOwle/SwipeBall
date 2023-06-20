using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip pic;
    [SerializeField] private ScreenToWorldCaster screenToWorldCaster;
    [SerializeField] private SleepCalculate sleepCalculate;

    private bool onTarget = false;

    public void Initialise(FollowTargetChanger follow, IChangeGameSate changeGameSate)
    {
        follow.EventFollowSetTarget += CameraChangeFollow;
        follow.SetDefaultTarget(transform);
        follow.SetTarget(transform);
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
        Debug.Log("Follow = " + value);
        if(value != this.transform)
        {
            onTarget = false;
            sleepCalculate.SelfDisable();
            screenToWorldCaster.SelfDisable();
        }
        else
        {
            onTarget = true;
            sleepCalculate.SelfEnable();
            screenToWorldCaster.SelfEnable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude < 1.5f) return;

        Vector3 normal = collision.contacts[0].normal;
        Quaternion rotation = Quaternion.LookRotation(normal);
        Pool.Instance.SpawnPooledObject<CartoonHit>(transform.position, rotation);
        PlaySoundEffect(hit);
    }

    private void ChangeGameSate(GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                sleepCalculate.SelfEnable();
                screenToWorldCaster.SelfEnable();
                break;
            case GameState.Pause:
                sleepCalculate.SelfDisable();
                screenToWorldCaster.SelfDisable();
                break;
        }
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        SoundController.Instance.PlayClipAtPosition(clip, transform.position);
    }

}
