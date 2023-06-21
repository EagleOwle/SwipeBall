using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldCaster : MonoBehaviour
{
    [SerializeField] private LayerMask ballMask;

    private Plane plane = new Plane(Vector3.up, 0);
    private Vector3 point;
    private Vector3 swipeDirection;
    private Ray ray;
    private Collider[] colliders;

    private void Update()
    {
        if (SwipeCalculate.Instance.OnSwipe)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                point = ray.GetPoint(distance);
                colliders = Physics.OverlapSphere(point, 1, ballMask);

                if (colliders.Length > 0)
                {
                    foreach (var item in colliders)
                    {
                        if (item.TryGetComponent(out Push push))
                        {
                            Push(push);
                        }
                    }

                    colliders = new Collider[0];
                    SwipeCalculate.Instance.ResetSwipe();
                }
            }
        }
    }

    private void Push(Push push)
    {
        swipeDirection = new Vector3(SwipeCalculate.Instance.SwipeScreenDirection.x,
                                     push.transform.position.y,
                                     SwipeCalculate.Instance.SwipeScreenDirection.y);
        swipeDirection = Camera.main.transform.TransformDirection(swipeDirection);
        push.OnPush(swipeDirection);
    }
}