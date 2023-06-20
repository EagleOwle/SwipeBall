using System.Collections;
using UnityEngine;

public class RocketFly : MonoBehaviour
{
    [SerializeField] private float maxRotationAngle = 10.0f;
    [SerializeField] private float timeChangeDirection = 1f;
    private float nextTime;
    private float currentSpeed;
    private Coroutine corutine;

    public void StartFly()
    {
        nextTime = Time.time + timeChangeDirection;
        corutine = StartCoroutine(Fly());
    }

    public void StopFly()
    {
        StopCoroutine(corutine);
    }

    private IEnumerator Fly()
    {
        currentSpeed = 1;

        while (true)
        {
            currentSpeed += Time.deltaTime;
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeChangeDirection;
                float randomAngleX = Random.Range(-maxRotationAngle, maxRotationAngle);
                float randomAngleY = Random.Range(-maxRotationAngle, maxRotationAngle);
                transform.Rotate(randomAngleX, randomAngleY, 0);
            }

            yield return null;
        }
    }

}