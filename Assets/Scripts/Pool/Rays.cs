using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rays : PoolElement
{

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
                ToPool();
            }
        }
    }
}
