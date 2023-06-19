using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class RayCastSystem : IEcsRunSystem
{
    private RuntimeData runtimeData;

    public void Run()
    {
       ref var raycastHandler = ref runtimeData.raycastData;
       ref var swipeHandler = ref  runtimeData.swipeData;

        raycastHandler.Collider = null;
        raycastHandler.HitPoint = Vector3.zero;

        if (swipeHandler.OnSwipe)
        {
            ScreenRayCast(ref swipeHandler, ref raycastHandler);
        }
    }

    private void ScreenRayCast(ref SwipeHandlerData swipeHandler, ref ScreenRaycastData raycastHandler)
    {
        Vector2 screenPosition = swipeHandler.CursorPosition;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPosition), out RaycastHit hit, float.MaxValue))
        {
            raycastHandler.Collider = hit.collider;
            raycastHandler.HitPoint = hit.point;

            DebugExtension.DebugCircle(raycastHandler.HitPoint, 0.5f);
        }
    }
}