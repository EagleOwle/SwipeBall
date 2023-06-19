using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler: MonoBehaviour
{
    [SerializeField] private Collider origineCollider;

    private void OnCollisionEnter(Collision collision)
    {
        EcsWorld ecsWorld = GameObject.FindObjectOfType<EcsStartup>().EcsWorld;
        var hit = ecsWorld.NewEntity();
        ref var collisionComponent = ref hit.Get<CollisionEventComponent>();
        collisionComponent.origineCollider = origineCollider;
        collisionComponent.otherCollider = collision.collider;
    }

    private void OnTriggerEnter(Collider other)
    {
        EcsWorld ecsWorld = GameObject.FindObjectOfType<EcsStartup>().EcsWorld;
        var hit = ecsWorld.NewEntity();
        ref var collisionComponent = ref hit.Get<CollisionEventComponent>();
        collisionComponent.origineCollider = origineCollider;
        collisionComponent.otherCollider = other;
    }

}
