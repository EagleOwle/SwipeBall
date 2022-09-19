using System;
using System.Collections;
using UnityEngine;

public class CarouselPrewievPlace : MonoBehaviour
{
    public Action<int> actionSetCurrentPoint;

    [SerializeField] private float speedRotation = 3;

    private PlacePoint[] points;
    private PlacePoint currentPoint;
    public PlacePoint CurrentPoint => currentPoint;

    private float nextAngle;
    private float oldAngle;

    private int pointCount;

    public void Initialise(int pointCount)
    {
        this.pointCount = pointCount;
        points = new PlacePoint[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            UIPresentViewe tmp = Instantiate(PrefabsStore.Instance.balls[i].viewePrefab);
            int angle = 360 / pointCount;
            tmp.transform.position = RandomCircle(transform.position, 0.3f, angle * i);
            tmp.transform.parent = transform;
            tmp.transform.localScale = Vector3.one * 0.1f;

            points[i].index = i;
            points[i].gameObject = tmp.gameObject;
            points[i].angle = angle * i;
        }

        SetCurrentPoint(nextAngle);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void GoNext()
    {
        StartRotation(nextAngle - (360 / pointCount));
    }

    public void GoPrevious()
    {
        StartRotation(nextAngle + (360 / pointCount));
    }

    private void SetCurrentPoint(float angle)
    {
        foreach (var item in points)
        {
            if(item.angle == angle)
            {
                currentPoint = item;
                actionSetCurrentPoint?.Invoke(currentPoint.index);
                if (currentPoint.gameObject.TryGetComponent(out VisualEffect effect))
                {
                    effect.ShowEffect();
                }
                return;
            } 
        }

        Debug.LogError("No Point at angle " + angle);
    }

    private void StartRotation(float angle)
    {
        oldAngle = nextAngle;

        if(currentPoint.gameObject.TryGetComponent(out VisualEffect effect))
        {
            effect.HideEffect();
        }

        StopAllCoroutines();
        StartCoroutine(Rotate(angle));
        nextAngle = ClampAngle(angle);
        SetCurrentPoint(nextAngle);
    }

    private IEnumerator Rotate(float targetAngle)
    {
        while (transform.rotation.y != targetAngle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), speedRotation * Time.deltaTime);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        yield return null;
    }

    private Vector3 RandomCircle(Vector3 center, float radius, int ange)
    {
        float ang = ange;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z - radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }

    private float ClampAngle(float angle)
    {
        if (angle >= 360f)
        {
            return angle - (360f * (int)(angle / 360f));
        }
        else if (angle >= 0f)
        {
            return angle;
        }
        else
        {
            float f = angle / -360f;
            int i = (int)f;
            if (f != i)
                ++i;
            return angle + (360f * i);
        }
    }

    #region Debug
    private void OnValidate()
    {
        pointCount = PrefabsStore.Instance.balls.Count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GoNext();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GoPrevious();
        }
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < pointCount; i++)
        {
            int angle = 360 / pointCount;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = RandomCircle(startPosition, 0.3f, angle * i);
            Gizmos.DrawLine(startPosition, endPosition);
        }
    }
    #endregion

    public struct PlacePoint
    {
        public int index;
        public GameObject gameObject;
        public int angle;
    }

}