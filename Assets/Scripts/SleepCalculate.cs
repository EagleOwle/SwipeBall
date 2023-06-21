using UnityEngine;
using System.Collections;
using System;

public class SleepCalculate : MonoBehaviour
{
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float maxSleepTime = 10;
    [SerializeField] private float minVelocityMagnitude = 0.3f;

    private float sleepTime;
    private float dbVelocity;

    private void OnEnable()
    {
        sleepTime = 0;
        tutorialCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        dbVelocity = rigidbody.velocity.magnitude;

        if (rigidbody.velocity.magnitude < minVelocityMagnitude)
        {
            if (sleepTime == 0)
            {
                sleepTime = maxSleepTime;
            }
        }
        else
        {
            if (sleepTime > 0)
            {
                tutorialCanvas.gameObject.SetActive(false);
            }

            sleepTime = 0;
        }

        if (sleepTime > 0)
        {
            sleepTime -= Time.deltaTime;

            if (sleepTime <= 0)
            {
                tutorialCanvas.gameObject.SetActive(true);
                sleepTime = 0;
            }
        }
    }

    public void SelfEnable()
    {
        enabled = true;
    }

    public void SelfDisable()
    {
        enabled = false;
        sleepTime = 0;
        tutorialCanvas.gameObject.SetActive(false);
    }

}
