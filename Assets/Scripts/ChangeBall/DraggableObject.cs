using UnityEngine;
using System.Collections;
using System;

public enum Side
{
    Center,
    Left,
    Right
}

public class DraggableObject : MonoBehaviour
{
    public Action<Side> actionOnEdge;
    [SerializeField] private float normalDistance = 1.5f;
    private const float speedDrag = 2;

    public void OnDragg(Vector2 direction)
    {
        if (Vector3.Distance(Vector3.zero, transform.localPosition) > normalDistance)
        {
            if (direction.x < 0)
            {
                actionOnEdge.Invoke(Side.Left);
            }
            else
            {
                if (direction.x > 0)
                {
                   actionOnEdge.Invoke(Side.Right);
                }
                else
                {
                  actionOnEdge.Invoke(Side.Center);
                }
            }
        }
        else
        {
            Vector3 position = transform.position;
            Vector3 dir = transform.parent.position + (Vector3)direction;
            position = Vector3.Lerp(position, dir, speedDrag * Time.deltaTime);
            transform.position = position;
        }
    }

    public void Reset()
    {
        StartCoroutine(MoveReset());
    }

    private IEnumerator MoveReset()
    {
        while(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, speedDrag * Time.deltaTime);
            yield return null;
        }
    }
}
