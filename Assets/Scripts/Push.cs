using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private float power = 10;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioClip push;

    public void OnPush(Vector3 direction)
    {
        direction = direction.normalized;
        rigidbody.AddForce(direction * power, ForceMode.VelocityChange);
        PlaySoundEffect(push);
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        SoundController.Instance.PlayClipAtPosition(clip, transform.position);
    }
}
