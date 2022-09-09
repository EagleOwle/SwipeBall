using UnityEngine;
using System.Collections;
using System;

public class SleepCalculate : MonoBehaviour
{
    public Action eventIsSleep;
    [SerializeField] private float maxSleepTime = 10;
    private float sleepTime;

    public void Enable(bool value)
    {
        enabled = value;
        sleepTime = 0;
    }

    private void Update()
    {
        sleepTime += Time.deltaTime;

        if (sleepTime >= maxSleepTime)
        {
            sleepTime = 0;
            eventIsSleep.Invoke();
        }

    }

    public void Break()
    {
        sleepTime = 0;
    }

}
