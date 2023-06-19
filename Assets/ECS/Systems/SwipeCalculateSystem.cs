using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class SwipeCalculateSystem : IEcsRunSystem
{
    private RuntimeData runtimeData;
    private StaticData staticData;
    private Vector2 tupPosition;
    private bool isDraggin = false;

    public void Run()
    {
        ControlInput();
        Swipe(ref runtimeData.swipeData);
    }

    private void Swipe(ref SwipeHandlerData swipeHandler)
    {
        if (isDraggin == false)
        {
            swipeHandler.CursorPosition = Vector2.zero;
            swipeHandler.TupPosition = Vector2.zero;
            swipeHandler.SwipeDirection = Vector2.zero;
            swipeHandler.OnSwipe = false;
            DrawScreenGizmos.Instance.DrawLine(swipeHandler.TupPosition, swipeHandler.TupPosition + swipeHandler.SwipeDirection);
            return;
        }

        swipeHandler.CursorPosition = (Vector2)Input.mousePosition;
        swipeHandler.TupPosition = tupPosition;
        swipeHandler.SwipeDirection = swipeHandler.CursorPosition - swipeHandler.TupPosition;

        if (swipeHandler.SwipeDirection.magnitude > staticData.DeadZone)
        {
            swipeHandler.OnSwipe = true;
        }
        else
        {
            swipeHandler.OnSwipe = false;
        }

        DrawScreenGizmos.Instance.DrawLine(swipeHandler.TupPosition, swipeHandler.TupPosition + swipeHandler.SwipeDirection);
    }

    private void OnDragging(Vector3 tupPosition)
    {
        isDraggin = true;
        this.tupPosition = tupPosition;
    }

    private void ControlInput()
    {
        if (!staticData.IsMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnDragging(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDraggin = false;
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    OnDragging(Input.touches[0].position);
                }

                if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    isDraggin = false;
                    ResetSwipe();
                }
            }
            else
            {
                isDraggin = false;
            }
        }
    }

    private void ResetSwipe()
    {
        isDraggin = false;
        tupPosition = Vector2.zero;
    }
}