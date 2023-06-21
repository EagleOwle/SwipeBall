using System.Collections;
using UnityEngine;

public class SmoothShowChild : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float showSpeed = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(ShowChild());
    }

    private IEnumerator ShowChild()
    {
        canvasGroup.alpha = 0;
        float dissolve = 0;
        while (dissolve < 1)
        {
            canvasGroup.alpha += showSpeed * Time.deltaTime;
            yield return null;
        }
    }

}