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
    private RaycastHit hit;
    private Ray ray;
    private Vector3 tupScreenPosition;

    private void Update()
    {
        if (SwipeCalculate.Instance.OnSwipe)
        {
            ray = Camera.main.ScreenPointToRay(SwipeCalculate.Instance.TupScreenPosition);

            if (Physics.Raycast(ray, out hit, 100, mazeMask))
            {
                tupScreenPosition = hit.point;
                //Debug.DrawLine(tupScreenPosition, tupScreenPosition + Vector3.up, Color.red, 1);
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, ballMask))
            {
                rigidbody.AddForce((transform.position - tupScreenPosition) * power);
                //Debug.DrawLine(tupScreenPosition, tupScreenPosition + (transform.position - tupScreenPosition), Color.red, 1);

                audioSource.PlayOneShot(push);
                SwipeCalculate.Instance.ResetSwipe();
            }
        }

    }
}
