using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPresent : Present
{
    [SerializeField] private int PresentPreferenceIndex;
    [SerializeField] private float speedMove = 5;
    [SerializeField] private float speedRotation = 25;
    [SerializeField] private float speedFade = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;
    [SerializeField] private Renderer renderer;
    private bool readyDestroy = false;

    private IEnumerator MoveToCamera()
    {
        while (true)
        {
            Vector3 camPosition = Camera.main.transform.position + Camera.main.transform.forward * 2;
            transform.position = Vector3.MoveTowards(transform.position, camPosition, speedMove * Time.deltaTime);
            Vector3 rotation = transform.rotation.eulerAngles + (Vector3.up * speedRotation);
            transform.rotation = Quaternion.Euler(rotation);
            yield return null;

            if(Vector3.Distance(transform.position, camPosition) <= 1)
            {
                ProcessFade();

                if(readyDestroy == false)
                {
                    readyDestroy = true;
                    Invoke(nameof(SelfDestroy), 5);
                }
               
            }
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void ProcessFade()
    {
        foreach (var item in renderer.materials)
        {
            float a = item.color.a;
            a -= speedFade * Time.deltaTime;
            a = Mathf.Clamp01(a);
            item.color = new Color(item.color.r, item.color.g, item.color.b, a);
        }
    }

    public override void Initialise()
    {
        Destroy(rigidbody);
        Destroy(collider);
        StartCoroutine(MoveToCamera());
        PrefabsStore.Instance.balls[PresentPreferenceIndex].acces = Acces.Available;
    }

    public override void EndOfLive()
    {
        
    }
}
