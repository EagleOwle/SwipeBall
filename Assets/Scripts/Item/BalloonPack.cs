using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BalloonPack : Present
{
    [SerializeField] private float balloonLiveTime = 5;
    [SerializeField] private Balloon[] balloons;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float curveMultiplier = 5;
    private List<Balloon> activeBalloons;
    private float curveTime;

    private void Awake()
    {
        balloons = GetComponentsInChildren<Balloon>();
        foreach (var item in balloons)
        {
            item.eventOnTouch.AddListener(BalloonOnTouch);
        }
    }

    private IEnumerator Fly()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            float speed = speedCurve.Evaluate(curveTime) * Time.deltaTime;
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, speed);
            rigidbody.MovePosition(nextPosition);

            if (transform.position.y > curveTime * curveMultiplier)
            {
                curveTime += 0.1f;
            }
        }
    }

    private void BalloonOnTouch(Balloon balloon)
    {
        balloon.Disable();

        activeBalloons.Remove(balloon);

        if (activeBalloons.Count == 0)
        {
            float length = 0;// audioSource.clip.length;
            Invoke(nameof(EndOfLive), length + Time.deltaTime);
        }
    }

    public override void Initialise()
    {
        GameObject.FindObjectOfType<FollowTargetChanger>().SetTarget(transform);
        for (int i = 0; i < balloons.Length; i++)
        {
            if (balloonLiveTime > 0)
            {
                balloons[i].Initialise((i + 1) * balloonLiveTime);
            }
            else
            {
                balloons[i].Initialise();
            }
        }

        activeBalloons = balloons.ToList();
        curveTime = 0;
        StartCoroutine(Fly());
    }

    public override void EndOfLive()
    {
        StopAllCoroutines();
        GameObject.FindObjectOfType<FollowTargetChanger>().SetTarget(null);
        Destroy(gameObject);
    }

}
