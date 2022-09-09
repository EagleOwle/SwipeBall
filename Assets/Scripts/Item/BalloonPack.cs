using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BalloonPack : PoolElement
{
    [SerializeField] private float liveTime = 5;
    [SerializeField] private Balloon[] balloons;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rigidbody;

    private List<Balloon> activeBalloons;

    private void Awake()
    {
        balloons = GetComponentsInChildren<Balloon>();
        foreach (var item in balloons)
        {
            item.eventOnTouch.AddListener(BalloonOnTouch);
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.y > 1)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void BalloonOnTouch(Balloon balloon)
    {
        balloon.Disable();

        activeBalloons.Remove(balloon);
        rigidbody.mass = activeBalloons.Count;

        if (activeBalloons.Count == 0)
        {
            float length = audioSource.clip.length;
            Invoke(nameof(EndOfLive), length + Time.deltaTime);
        }
    }

    public override void FromPool()
    {
       GameObject.FindObjectOfType<Follow>().Target = transform;
        for (int i = 0; i < balloons.Length; i++)
        {
            if (liveTime > 0)
            {
                balloons[i].Initialise((i + 1) * liveTime, audioSource);
            }
            else
            {
                balloons[i].Initialise(audioSource);
            }
        }

        activeBalloons = balloons.ToList();
        rigidbody.mass = activeBalloons.Count;
        //Debug.Break();
    }

    private void EndOfLive()
    {
        GameObject.FindObjectOfType<Follow>().Target = null;
        ToPool();
    }

}
