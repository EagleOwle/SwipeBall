using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class SetRandomColorSystem : IEcsRunSystem
{
    private EcsFilter<RandomColorComponent> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var component = ref filter.Get1(i);
            int rnd = Random.Range(0, component.materials.Length);
            component.renderer.material = component.materials[rnd];
        }
    }
}