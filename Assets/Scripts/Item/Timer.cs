using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action<Timer> EventEndTime;

    private float targetTime;

    public void StartTimer(float targetTime)
    {
        this.targetTime = Time.time + targetTime;
        StartCoroutine(TimerUpdate());
    }

    private IEnumerator TimerUpdate()
    {
        while(Time.time < targetTime)
        {
            yield return null;
        }

        EventEndTime?.Invoke(this);
    }

}