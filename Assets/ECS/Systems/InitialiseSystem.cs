using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private StaticData staticData;
    private SceneData sceneData;
    private RuntimeData runtimeData;

    public void Init()
    {
        EcsEntity playerEntity = ecsWorld.NewEntity();
        ref var ballMotion = ref playerEntity.Get<BallMotionComponent>();
        ref var playerInput = ref playerEntity.Get<PlayerInputComponent>();
        GameObject playerGO = Object.Instantiate(staticData.playerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
        ballMotion.rigidbody = playerGO.GetComponent<Rigidbody>();
        ballMotion.speed = staticData.playerSpeed;

        EcsEntity cameraEntity = ecsWorld.NewEntity();
        ref var lookAt = ref cameraEntity.Get<LookAtComponent>();
        lookAt.movableCameraTransform = sceneData.movableCameraTransform;
        lookAt.lookbleCameraTransform = sceneData.lookbleCameraTransform;
        lookAt.targetTransform = playerGO.transform;

        ref var fallow = ref cameraEntity.Get<SmoothFallowComponent>();
        fallow.selfTransform = sceneData.movableCameraTransform;
        fallow.targetTransform = playerGO.transform;
        fallow.speed = staticData.followSpeed;
        fallow.offset = Vector3.up * 8;

    }
}
