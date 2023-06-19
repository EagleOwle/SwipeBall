using System;
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

    public void SelfEnable()
    {
        enabled = true;
    }

    public void SelfDisable()
    {
        enabled = false;
    }

    private void Update()
    {
        if (SwipeCalculate.Instance.OnSwipe)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, Mathf.Infinity, ballMask))
            {
                Vector3 direction = new Vector3(SwipeCalculate.Instance.SwipeScreenDirection.x, 0, SwipeCalculate.Instance.SwipeScreenDirection.y);

                direction = Camera.main.transform.TransformDirection(direction);
                
                OnPush(direction);
                SwipeCalculate.Instance.ResetSwipe();
            }
        }
    }

    private void OnPush(Vector3 direction)
    {
        direction.y = transform.position.y;
        Debug.DrawLine(transform.position, transform.position + (-direction * power), Color.red, 1);
        rigidbody.AddForce(direction * power, ForceMode.VelocityChange);
        audioSource.PlayOneShot(push);
    }

}
