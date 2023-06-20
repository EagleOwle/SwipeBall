using System.Collections;
using UnityEngine;

public class SpawnSurprize : MonoBehaviour
{
    private Present nextPrefab;

    public void Spawn(Present prefab)
    {
        nextPrefab = prefab;

        if (prefab is Rocket)
        {
            Invoke(nameof(SpawnRocket), Random.Range(0f, 1f));
            Invoke(nameof(SpawnRocket), Random.Range(0f, 1f));
            Invoke(nameof(SpawnRocket), Random.Range(0f, 1f));
        }

        if (prefab is BalloonPack)
        {
            SpawnBaloon();
        }
    }

    private void SpawnBaloon()
    {
        Present present = Instantiate(nextPrefab, transform.position, Quaternion.identity);
        present.Initialise();
    }

    private void SpawnRocket()
    {
        Vector3 position = new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
        Present present = Instantiate(nextPrefab, transform.position + position, Quaternion.identity);
        present.Initialise();
    }

}