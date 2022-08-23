using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform defaultTarget;
    private Transform target;
    public Transform Target
    {
        set
        {
            if(value == null)
            {
                target = defaultTarget;
            }
            else
            {
                target = value;
            }

            EventSpace.SetFollowTarget.Invoke(target);
        }
    }

    [SerializeField] private float smooth = 1;
    [SerializeField] private float distanceBase = 5f;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        target = defaultTarget;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Follow target is null");
            return;
        }

        transform.LookAt(target.position);
        FollowDirection(target.position);
    }

    private void FollowDirection(Vector3 targetPosition)
    {
        Vector3 direction = (transform.position - targetPosition).normalized;
        Vector3 nextPosition = targetPosition + (direction * distanceBase);

        Debug.DrawLine(transform.position, nextPosition, Color.red);
        nextPosition.y = Mathf.Clamp(nextPosition.y, 3, 5);
        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * smooth);
        
    }
}
