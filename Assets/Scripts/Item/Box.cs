using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationEvent
{
    void AnimationStateEvent();
}

public class Box : Item, IAnimationEvent
{
    public Material[] materials;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Animator animator;
    [SerializeField] private float hideSpeed = 0.1f;
    private int openParamID;
    private float dissolve = 0;

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

        //Pool.Instance.SpawnPooledObject(PoolElementType.Firework, transform.position, Quaternion.identity);
        Pool.Instance.SpawnPooledObject(PoolElementType.Hit, transform.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Pool.Instance.SpawnPooledObject(PoolElementType.Simple, transform.position, rotation);


        actionOnHit.Invoke(this);
        animator.SetTrigger(openParamID);
        animator.speed = 1;
        // Destroy(gameObject);

        Invoke(nameof(ShowSurprize), 1);
    }

    private void ShowSurprize()
    {
        Pool.Instance.SpawnPooledObject(PoolElementType.Ballon, transform.position + Vector3.up, Quaternion.identity);
    }

    private IEnumerator WaitOfAlpha()
    {
        //dissolve = 1;
        while (dissolve < 1)
        {
            yield return new WaitForEndOfFrame();
            renderer.material.SetFloat("_Dissolve", dissolve);
            dissolve += hideSpeed * Time.deltaTime;
            
        }

        //renderer.material.SetFloat("_Dissolve", 0);
        Debug.Log("End While");
    }

    public void AnimationStateEvent()
    {
        StartCoroutine(WaitOfAlpha());
    }

}
