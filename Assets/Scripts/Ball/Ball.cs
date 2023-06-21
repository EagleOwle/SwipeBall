using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip pic;

    public bool CameraTarget => cameraTarget;
    private bool cameraTarget = false;

    public void Initialise(FollowTargetChanger follow, IChangeGameState changeGameSate)
    {
        follow.EventFollowSetTarget += CameraChangeFollow;
        follow.SetDefaultTarget(transform);
        follow.SetTarget(transform);

        changeGameSate.EventOnChangeGameState += ChangeGameSate_EventOnChangeGameState;

        cameraTarget = true;
        tutorialObj.SetActive(true);
    }

    private void ChangeGameSate_EventOnChangeGameState(GameState value)
    {
        switch (value)
        {
            case GameState.Game:
                if (cameraTarget == true)
                {
                    tutorialObj.SetActive(true);
                }
                break;
            case GameState.Pause:
                tutorialObj.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cameraTarget == false) return;

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
        if (value != this.transform)
        {
            cameraTarget = false;
            tutorialObj.SetActive(false);
        }
        else
        {
            cameraTarget = true;
            tutorialObj.SetActive(true);
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


    private void PlaySoundEffect(AudioClip clip)
    {
        SoundController.Instance.PlayClipAtPosition(clip, transform.position);
    }

}
