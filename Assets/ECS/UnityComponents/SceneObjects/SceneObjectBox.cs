using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectBox : SceneObject
{
    public float DissolveSpeed => dissolveSpeed;
    [SerializeField] private float dissolveSpeed = 0.5f;

    [SerializeField] private Animator animator;

    private int openParamID;

    private void Awake()
    {
        openParamID = Animator.StringToHash("Open");
    }

    private void Start()
    {
        EcsWorld ecsWorld = GameObject.FindObjectOfType<EcsStartup>().EcsWorld;
        var entity = ecsWorld.NewEntity();

        ref var sceneObjectComponent = ref entity.Get<SceneObjectComponent>();
        ref var phisicsHandlerComponent = ref entity.Get<PhisicsHandlerComponent>();

        sceneObjectComponent.gameObject = gameObject;
        phisicsHandlerComponent.collider = GetComponent<Collider>();
        phisicsHandlerComponent.onCollision = false;
    }

    public void ShowOpenBox()
    {
        animator.SetTrigger(openParamID);
        animator.speed = 1;
    }
}
