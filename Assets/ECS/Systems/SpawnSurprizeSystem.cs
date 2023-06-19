using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class SpawnSurprizeSystem : IEcsRunSystem
{
    private EcsFilter<PhisicsHandlerComponent, SceneObjectComponent> sceneObjectEntity;

    public void Run()
    {
        foreach (var i in sceneObjectEntity)
        {
            ref var phisicsHandlerComponent = ref sceneObjectEntity.Get1(i);
            ref var sceneObjectComponent = ref sceneObjectEntity.Get2(i);

            if (phisicsHandlerComponent.onCollision)
            {
                ref EcsEntity entity = ref sceneObjectEntity.GetEntity(i);
                ref var processDissolveComponent = ref entity.Get<ProcessDissolveComponent>();
                SceneObjectBox sceneObjectBox = sceneObjectComponent.gameObject.GetComponentInChildren<SceneObjectBox>();
                processDissolveComponent.renderer = sceneObjectComponent.gameObject.GetComponentInChildren<Renderer>();
                processDissolveComponent.dissolve = 0;
                processDissolveComponent.dissolveSpeed = sceneObjectBox.DissolveSpeed;

                sceneObjectBox.ShowOpenBox();
                Debug.Log("Add Dissolve Effect");
            }
           
        }
    }
}