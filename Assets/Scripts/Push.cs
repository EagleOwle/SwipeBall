using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private float power = 10;
    [SerializeField] private LayerMask ballMask;
    [SerializeField] private LayerMask mazeMask;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip push;
    [SerializeField] private SleepCalculate sleepCalculate;

    private RaycastHit hit;
    private Ray ray;
    private Vector3 tupScreenPosition;

    public void Enable(bool value)
    {
        sleepCalculate.Enable(value);
        enabled = value;
    }

    private void Update()
    {
        if (SwipeCalculate.Instance.OnSwipe)
        {
            sleepCalculate.Break();

            ray = Camera.main.ScreenPointToRay(SwipeCalculate.Instance.TupScreenPosition);

            if (Physics.Raycast(ray, out hit, 100, mazeMask))
            {
                tupScreenPosition = hit.point;
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, ballMask))
            {
                rigidbody.AddForce((transform.position - tupScreenPosition) * power);

                audioSource.PlayOneShot(push);
                SwipeCalculate.Instance.ResetSwipe();
            }
        }

    }
    

}
