using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldCaster : MonoBehaviour
{
    [SerializeField] private Push push;
    [SerializeField] private LayerMask mazeMask;
    [SerializeField] private LayerMask ballMask;

    private List<Vector3> points = new List<Vector3>();
    private Plane plane = new Plane(Vector3.up, 0);
    private Vector3 point;
    private Vector3 swipeDirection;
    private Ray ray;

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
            if (plane.Raycast(ray, out float distance))
            {
                point = ray.GetPoint(distance);
                points.Add(point);

                Collider[] colliders = Physics.OverlapSphere(point, 1, ballMask);

                if (colliders.Length > 0)
                {
                    swipeDirection = new Vector3(SwipeCalculate.Instance.SwipeScreenDirection.x,
                                                 transform.position.y, 
                                                 SwipeCalculate.Instance.SwipeScreenDirection.y);
                    swipeDirection = Camera.main.transform.TransformDirection(swipeDirection);
                    push.OnPush(swipeDirection);
                    SwipeCalculate.Instance.ResetSwipe();
                }

            }

            //if (ScreenToWorldPosition(Input.mousePosition, out Vector3 position))
            //{
            //    position.y = transform.position.y;
            //    points.Add(position);

            //    Ray ray = new Ray(Camera.main.transform.position, position - Camera.main.transform.position);
            //    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ballMask))
            //    {
            //        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green);
            //        Vector3 swipeDirection = new Vector3(SwipeCalculate.Instance.SwipeScreenDirection.x, 0, SwipeCalculate.Instance.SwipeScreenDirection.y);
            //        swipeDirection = Camera.main.transform.TransformDirection(swipeDirection);
            //        push.OnPush(swipeDirection);
            //        SwipeCalculate.Instance.ResetSwipe();
            //    }
            //    else
            //    {
            //        Debug.DrawRay(Camera.main.transform.position, position - Camera.main.transform.position, Color.blue);
            //    }
            //}
        }
        else
        {
            points.Clear();
        }
    }

    private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mazeMask))
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.blue);
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private bool ScreenToWorldPosition(Vector2 screenPosition, out Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mazeMask))
        {
            position = hit.point;
            return true;
        }
        else
        {
            position = Vector3.zero;
            return false;
        }
    }

    private void Line(Vector3 position)
    {
        position.y = transform.position.y;
        points.Add(position);

        if (points.Count > 1)
        {
            Ray ray = new Ray(points[points.Count - 1], points[points.Count - 1] - points[points.Count - 2]);
            if (Physics.SphereCast(points[points.Count - 1],
                                1,
                                ray.direction,
                                out RaycastHit hit,
                                ray.direction.magnitude,
                                ballMask))
            {
                //Debug.DrawRay(points[points.Count - 1], ray.direction, Color.green, 2);
                push.OnPush(ray.direction);
                SwipeCalculate.Instance.ResetSwipe();
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.DrawLine(points[i], points[i] + Vector3.up, Color.white, 1);
        }

        for (int i = 0; i < points.Count; i++)
        {
            if (i + 1 < points.Count)
            {
                Debug.DrawLine(points[i], points[i + 1], Color.white, 1);
            }
        }

    }

}