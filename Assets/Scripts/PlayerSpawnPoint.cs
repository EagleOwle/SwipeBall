using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Ball SpawnPlayer()
    {
        return Instantiate(PrefabsStore.Instance.CurrentBallPrefab, transform.position + Vector3.up, Quaternion.identity);
    }
}
