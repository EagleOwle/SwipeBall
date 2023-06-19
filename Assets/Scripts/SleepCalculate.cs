using UnityEngine;
using System.Collections;
using System;

public class SleepCalculate : MonoBehaviour
{
    [SerializeField] private TutorialMenu tutorialMenu;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float maxSleepTime = 10;
    [SerializeField] private float minVelocityMagnitude = 0.3f;

    private float sleepTime;
    private float dbVelocity;

    private void Start()
    {
        sleepTime = 0;
        tutorialMenu.Hide();
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
                tutorialMenu.Hide();
            }

            sleepTime = 0;
        }

        if (sleepTime > 0)
        {
            sleepTime -= Time.deltaTime;

            if (sleepTime <= 0)
            {
                tutorialMenu.Show();
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
        tutorialMenu.Hide();
    }

}
