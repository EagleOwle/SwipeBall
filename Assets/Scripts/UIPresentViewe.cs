using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VisualEffect
{
    void ShowEffect();
    void HideEffect();
}

public class UIPresentViewe : MonoBehaviour, VisualEffect
{
    [SerializeField] private float speedMove = 5;
    [SerializeField] private float speedRotation = 25;
    [SerializeField] private float speedScele = 25;
    [SerializeField] private Transform effectTarget;
    private Vector3 baseScale;
    private bool showEffect = false;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    public void Initialise()
    {
        ShowEffect();
    }

    public void HideEffect()
    {
        showEffect = false;
        StopCoroutine(Rotation());
        transform.localScale = baseScale;
    }

    public void ShowEffect()
    {
        showEffect = true;
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        while (showEffect)
        {
            Vector3 rotation = effectTarget.localRotation.eulerAngles + (Vector3.up * speedRotation);
            effectTarget.localRotation = Quaternion.Euler(rotation);
            yield return null;
        }
    }


}
