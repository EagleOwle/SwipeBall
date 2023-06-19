using Leopotam.Ecs;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour
{
    public StaticData configuration;
    public SceneData sceneData;
    public RuntimeData runtimeData;

    public EcsWorld EcsWorld => ecsWorld;
    private EcsWorld ecsWorld;
    private EcsSystems initSystems;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;
    private EcsSystems lateUpdateSystems;

    void Start()
    {
        ecsWorld = new EcsWorld();
        initSystems = new EcsSystems(ecsWorld);
        updateSystems = new EcsSystems(ecsWorld);
        fixedUpdateSystems = new EcsSystems(ecsWorld);
        lateUpdateSystems = new EcsSystems(ecsWorld);


#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(lateUpdateSystems);
#endif

        initSystems
        .Add(new InitialiseSystem())
        .Inject(configuration)
        .Inject(sceneData);

        updateSystems
        .Add(new SetRandomColorSystem())
        .Add(new PlayerInputSystem())
        .Add(new SwipeCalculateSystem())
        .Add(new RayCastSystem())
        .Add(new CollisionEventSystem())
        .Add(new SpawnSurprizeSystem())
        .Add(new DissolveEffectSystem())
        .OneFrame<CollisionEventComponent>()
        .OneFrame<RandomColorComponent>()
        .Inject(configuration)
        .Inject(sceneData)
        .Inject(runtimeData);

        fixedUpdateSystems
        .Add(new BallMoveSystem())
        .Add(new PushRiggidbodySystem())
        .Inject(sceneData)
        .Inject(runtimeData);

        lateUpdateSystems
        .Add(new LookAtSystem())
        .Add(new SmoothFallowSystem())
        .Add(new EventCollisionResetSystem());

        initSystems.ProcessInjects();
        updateSystems.ProcessInjects();
        fixedUpdateSystems.ProcessInjects();
        lateUpdateSystems.ProcessInjects();

        initSystems.Init();
        updateSystems.Init();
        fixedUpdateSystems.Init();
        lateUpdateSystems.Init();
    }

    void Update()
    {
        updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems?.Run();
    }

    private void LateUpdate()
    {
        lateUpdateSystems?.Run();
    }

    void OnDestroy()
    {
        if (initSystems != null)
        {
            initSystems.Destroy();
        }

        if (updateSystems != null)
        {
            updateSystems?.Destroy();
            updateSystems = null;
        }

        if (fixedUpdateSystems != null)
        {
            fixedUpdateSystems?.Destroy();
            fixedUpdateSystems = null;
        }

        if (lateUpdateSystems != null)
        {
            lateUpdateSystems?.Destroy();
            lateUpdateSystems = null;
        }

        if (ecsWorld != null)
        {
            ecsWorld?.Destroy();
            ecsWorld = null;
        }
    }
}