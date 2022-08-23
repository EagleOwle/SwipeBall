using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BalloonPack : PoolElement
{
    [SerializeField] private float liveTime = 5;
    [SerializeField] private Balloon[] balloons;

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

        foreach (var item in balloons)
        {
            if (item.gameObject.activeSelf)
            {
                return;
            }
        }

        Invoke(nameof(EndOfLive), 3);
    }

    public override void Instantiate()
    {
        GameObject.FindObjectOfType<Follow>().Target = transform;
        for (int i = 0; i < balloons.Length; i++)
        {
            balloons[i].gameObject.SetActive(true);
            balloons[i].Initialise((i + 1) * liveTime);
        }
    }

    private void EndOfLive()
    {
        GameObject.FindObjectOfType<Follow>().Target = null;
        ToPool();
    }

}
