using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class EventCollisionResetSystem : IEcsRunSystem
{
    private EcsFilter<PhisicsHandlerComponent> phisicsHandlerComponent;

    public void Run()
    {
        foreach (var i in phisicsHandlerComponent)
        {
            ref var phisicsHandler = ref phisicsHandlerComponent.Get1(i);
            phisicsHandler.onCollision = false;
        }
    }
}