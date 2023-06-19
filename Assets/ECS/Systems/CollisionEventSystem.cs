using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventSystem : IEcsRunSystem
{
    private EcsFilter<CollisionEventComponent> collisionsEntity;
    private EcsFilter<PhisicsHandlerComponent> phisicsHandlerComponent;

    public void Run()
    {
        foreach (var i in collisionsEntity)
        {
            ref var collisionsComponent = ref collisionsEntity.Get1(i);

            foreach (var j in phisicsHandlerComponent)
            {
                ref var phisicsHandler = ref phisicsHandlerComponent.Get1(j);

                if(collisionsComponent.otherCollider == phisicsHandler.collider)
                {
                    phisicsHandler.onCollision = true;
                }
            }
        }
    }
}
