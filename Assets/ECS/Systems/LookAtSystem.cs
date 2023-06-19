using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSystem : IEcsRunSystem
{
    private EcsFilter<LookAtComponent> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var component = ref filter.Get1(i);
            component.movableCameraTransform.LookAt(component.targetTransform);
            component.movableCameraTransform.eulerAngles = new Vector3(0, component.movableCameraTransform.eulerAngles.y, 0);

            component.lookbleCameraTransform.LookAt(component.targetTransform);

        }
    }
}
