using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private float hideSpeed = 0.5f;

    public void StartDissolve()
    {
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        float dissolve = 0;
        while (dissolve < 1)
        {
            yield return new WaitForEndOfFrame();
            renderer.material.SetFloat("_Dissolve", dissolve);
            dissolve += hideSpeed * Time.deltaTime;

        }

        Destroy(gameObject);
    }
}
