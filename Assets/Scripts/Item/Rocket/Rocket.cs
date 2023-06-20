using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Present, IInitialiser
{
    [SerializeField] private RocketFly rocketFly;
    [SerializeField] private GameObject renderObj;
    [SerializeField] private ParticleSystem sparksRainParticles;
    [SerializeField] private AudioClip fly;
    [SerializeField] private AudioClip shoot;

    public override void EndOfLive()
    {
        GameObject.FindObjectOfType<FollowTargetChanger>().SetTarget(null);
        Destroy(gameObject);
    }

    public override void Initialise()
    {
        GameObject.FindObjectOfType<FollowTargetChanger>().SetTarget(transform);
        transform.rotation = Quaternion.LookRotation(Vector3.up);
        rocketFly.StartFly();

        AddComponentLiveTimer();

        SoundController.Instance.PlayClipFollowTransform(fly, transform);

    }

    private void AddComponentLiveTimer()
    {
        Timer timer = gameObject.AddComponent<Timer>();
        timer.EventEndTime += Timer_EventEndTime;
        timer.StartTimer(Random.Range(2.8f, 3.3f));
    }

    private void Timer_EventEndTime(Timer timer)
    {
        SoundController.Instance.PlayClipAtPosition(shoot, transform.position);
        if (Pool.Instance != null)
        {
            Pool.Instance.SpawnPooledObject<Fireworks>(transform.position, Quaternion.identity);
        }

        timer.EventEndTime -= Timer_EventEndTime;

        rocketFly.StopFly();
        renderObj.SetActive(false);
        sparksRainParticles.Stop();
        Invoke(nameof(EndOfLive), 1.5f);
    }

}
