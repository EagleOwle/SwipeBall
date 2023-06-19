using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class DissolveEffectSystem : IEcsRunSystem
{
    private EcsFilter<ProcessDissolveComponent> processDissolveEntity;

    public void Run()
    {
        foreach (var i in processDissolveEntity)
        {
            ref var processDissolveComponent = ref processDissolveEntity.Get1(i);
            float dissolve = processDissolveComponent.dissolve;
            float speed = processDissolveComponent.dissolveSpeed;

            if (dissolve < 1)
            {
                dissolve += speed * Time.deltaTime;
                processDissolveComponent.renderer.material.SetFloat("_Dissolve", dissolve);
                processDissolveComponent.dissolve = dissolve;
            }
            else
            {
                ref EcsEntity entity = ref processDissolveEntity.GetEntity(i);
                entity.Del<ProcessDissolveComponent>();
            }
        }
    }
}