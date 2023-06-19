using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class SmoothFallowSystem : IEcsRunSystem
{
    private EcsFilter<SmoothFallowComponent> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var component = ref filter.Get1(i);
            SmoothFollow(ref component);
        }
    }

    private void SmoothFollow(ref SmoothFallowComponent component)
    {
        Vector3 from = component.selfTransform.position;
        Vector3 to = component.targetTransform.position + component.offset;
        component.selfTransform.position = Vector3.Lerp(from, to, component.speed * Time.deltaTime);
    }

}