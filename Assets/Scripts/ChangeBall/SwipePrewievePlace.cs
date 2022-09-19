using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePrewievePlace : MonoBehaviour
{
    [SerializeField] private CarouselPrewievPlace carouselPrewievPlace;
    [SerializeField] private float allowedRadius;
    [SerializeField] private LayerMask mask;

    private Ray ray;
    private RaycastHit hit;
    private bool isMobilePlatform;
    private Vector2 debugDirection;
    private Vector2 center;
    private Vector2 tapPosition;
    private bool isDraggin;
    private DraggableObject draggableObject;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isMobilePlatform = false;
#else 
        isMobilePlatform = true;
#endif
    }

    private void Update()
    {
        ControlInput();
        Dragg();
    }

    private void ControlInput()
    {
        if (!isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnTap(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    OnTap(Input.touches[0].position);
                }

                if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    Reset();
                }
            }
        }
    }

    private void Dragg()
    {
        if (isDraggin == false) return;

        Vector3 direction = (Vector2)Input.mousePosition - tapPosition;
        direction.y = 0;
        direction.x = (direction.x / (Screen.width / 2));
        debugDirection = direction;
        DrawScreenGizmos.Instance.DrawLine(Input.mousePosition, tapPosition);
        draggableObject.OnDragg(direction);
    }

    private bool InRadius(Vector2 position)
    {
        center = new Vector2(Screen.width / 2, Screen.height / 2);

        if (Vector2.Distance(position, center) < allowedRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTap(Vector3 tapPosition)
    {
        if (InRadius(tapPosition))
        {
            this.tapPosition = tapPosition;
            if (ScreenRayCast(tapPosition, mask))
            {
                isDraggin = true;
            }
        }
    }

    private bool ScreenRayCast(Vector2 screenPosition, LayerMask mask)
    {
        ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
        {
            if (hit.collider.TryGetComponent(out DraggableObject draggableObject))
            {
                if (this.draggableObject != null)
                {
                    this.draggableObject.actionOnEdge -= DraggableObjectEvent;
                }

                this.draggableObject = draggableObject;
                this.draggableObject.actionOnEdge += DraggableObjectEvent;
                return true;
            }
        }

        return false;
    }

    private void DraggableObjectEvent(Side side)
    {
        switch (side)
        {
            case Side.Center:
                Reset();
                break;
            case Side.Left:
                carouselPrewievPlace.GoPrevious();
                Reset(); 
                break;
            case Side.Right:
                carouselPrewievPlace.GoNext();
                Reset();
                break;
        }
    }

    private void Reset()
    {
        isDraggin = false;
        tapPosition = Vector2.zero;
        if (draggableObject != null)
        {
            draggableObject.actionOnEdge -= DraggableObjectEvent;
            draggableObject.Reset();
            draggableObject = null;
        }
    }

    private void OnDisable()
    {
        Reset();
    }

}
