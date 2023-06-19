using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class BallMoveSystem : IEcsRunSystem
{
    private EcsFilter<BallMotionComponent, PlayerInputComponent> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var player = ref filter.Get1(i);
            ref var input = ref filter.Get2(i);

            Vector3 direction = (Vector3.forward * input.moveInput.z + Vector3.right * input.moveInput.x).normalized;
            player.rigidbody.AddForce(direction * player.speed);
            Debug.DrawLine(player.rigidbody.position, player.rigidbody.position + player.rigidbody.velocity, Color.white, 1);
        }
    }
}