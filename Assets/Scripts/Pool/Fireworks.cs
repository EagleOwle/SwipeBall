using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireworks : PoolElement
{
    public UnityEvent eventEndLive;

    public override void FromPool()
    {
        StartCoroutine(CheckIfAlive());
    }

    private IEnumerator CheckIfAlive()
    {
        ParticleSystem particles = this.GetComponent<ParticleSystem>();

        while (true && particles != null)
        {
            yield return new WaitForSeconds(0.5f);

            if (particles.IsAlive(true) == false)
            {
                eventEndLive.Invoke();
                break;
            }
        }
    }

    
}
