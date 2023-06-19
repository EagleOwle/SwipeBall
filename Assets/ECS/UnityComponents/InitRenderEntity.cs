using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class InitRenderEntity : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Material[] materials;

    private void Start()
    {
        EcsWorld ecsWorld = GameObject.FindObjectOfType<EcsStartup>().EcsWorld;
        var entity = ecsWorld.NewEntity();
        ref var materialComponent = ref entity.Get<RandomColorComponent>();
        materialComponent.renderer = renderer;
        materialComponent.materials = materials;
        Destroy(this);
    }

}