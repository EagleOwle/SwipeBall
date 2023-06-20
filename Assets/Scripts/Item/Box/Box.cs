using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{
    public Material[] materials;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpawnSurprize spawnSurprize;
    [SerializeField] private float hideSpeed = 0.5f;
    private int openParamID;

    private void Awake()
    {
        openParamID = Animator.StringToHash("Open");
    }

    private void OnEnable()
    {
        int rnd = Random.Range(0, materials.Length);
        renderer.material = materials[rnd]; 

        animator.speed = Random.Range(0.3f, 0.5f);
    }

    public override void OnHit()
    {
        if (Pool.Instance == null)
        {
            Debug.LogWarning("ObjectPool.Instance is null");
            return;
        }

        Pool.Instance.SpawnPooledObject<CartoonHit>(transform.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Pool.Instance.SpawnPooledObject<Rays>(transform.position, rotation);


        actionOnHit.Invoke(this);
        animator.SetTrigger(openParamID);
        animator.speed = 1;

        ShowSurprize();
        StartCoroutine(WaitOfAlpha());
    }

    private void ShowSurprize()
    {
        spawnSurprize.Spawn(PrefabsStore.Instance.RandomPresent);
    }

    private IEnumerator WaitOfAlpha()
    {
       float dissolve = 0;
        while (dissolve < 1)
        {
            yield return new WaitForEndOfFrame();
            renderer.material.SetFloat("_DissolveAmount", dissolve);
            //renderer.material.SetFloat("_DissolveAmount", Mathf.Sin(Time.time) / 2 + 0.5f);
            dissolve += hideSpeed * Time.deltaTime;
            
        }

        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
