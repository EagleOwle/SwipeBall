using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class PushRiggidbodySystem : IEcsRunSystem
{
    private RuntimeData runtimeData;
    private SceneData sceneData;
    private EcsFilter<BallMotionComponent> filter;

    public void Run()
    {
        if (runtimeData.raycastData.Collider == null) return;

        foreach (var i in filter)
        {
            ref var ballMotion = ref filter.Get1(i);

            Collider collider = runtimeData.raycastData.Collider;

            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                if (rigidbody == ballMotion.rigidbody)
                {
                    Vector3 direction = new Vector3(runtimeData.swipeData.SwipeDirection.x, 0, runtimeData.swipeData.SwipeDirection.y);
                    direction = sceneData.movableCameraTransform.TransformDirection(direction);
                    ballMotion.rigidbody.AddForce(direction);
                    Debug.DrawLine(rigidbody.position, rigidbody.position + rigidbody.velocity, Color.white, 1);
                }
            }

        }
    }

   
}