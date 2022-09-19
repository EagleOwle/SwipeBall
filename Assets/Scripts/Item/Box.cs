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

        Pool.Instance.SpawnPooledObject(PoolElementType.Hit, transform.position, Quaternion.identity);
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Pool.Instance.SpawnPooledObject(PoolElementType.Simple, transform.position, rotation);


        actionOnHit.Invoke(this);
        animator.SetTrigger(openParamID);
        animator.speed = 1;
        

        Invoke(nameof(ShowSurprize), 1);
    }

    private void ShowSurprize()
    {
        Present present = Instantiate(PrefabsStore.Instance.RandomPresent, transform.position + Vector3.up, Quaternion.identity);
        present.Initialise();
    }

    private IEnumerator WaitOfAlpha()
    {
       float dissolve = 0;
        while (dissolve < 1)
        {
            yield return new WaitForEndOfFrame();
            renderer.material.SetFloat("_Dissolve", dissolve);
            dissolve += hideSpeed * Time.deltaTime;
            
        }

        Destroy(gameObject);
    }

    public void AnimationStateEvent()
    {
        StartCoroutine(WaitOfAlpha());
    }

}
