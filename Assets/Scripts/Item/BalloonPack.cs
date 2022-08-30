using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BalloonPack : PoolElement
{
    [SerializeField] private float liveTime = 5;
    [SerializeField] private Balloon[] balloons;

    [SerializeField] private Rigidbody rigidbody;

    private void Awake()
    {
        balloons = GetComponentsInChildren<Balloon>();
        foreach (var item in balloons)
        {
            item.eventOnTouch.AddListener(BalloonOnTouch);
        }
    }

    private void BalloonOnTouch(Balloon balloon)
    {
        balloon.gameObject.SetActive(false);

        float nextMass = rigidbody.mass - 1;
        nextMass = Mathf.Clamp(nextMass, 1, balloons.Length);
        rigidbody.mass = nextMass;

        foreach (var item in balloons)
        {
            if (item.gameObject.activeSelf)
            {
                return;
            }
        }

        EndOfLive();
    }

    public override void Instantiate()
    {
        GameObject.FindObjectOfType<Follow>().Target = transform;
        for (int i = 0; i < balloons.Length; i++)
        {
            balloons[i].gameObject.SetActive(true);

            if (liveTime > 0)
            {
                balloons[i].Initialise((i + 1) * liveTime);
            }
            else
            {
                balloons[i].Initialise();
            }
        }

        rigidbody.mass = balloons.Length;

    }

    private void EndOfLive()
    {
        GameObject.FindObjectOfType<Follow>().Target = null;
        ToPool();
    }

}
