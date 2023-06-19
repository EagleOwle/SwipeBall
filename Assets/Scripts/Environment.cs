using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IItemCount
{
    event Action<int> ChangeItemCount;
    event Action EndItem;
    int CurrentItemCount();
}

public class Environment : MonoBehaviour, IItemCount
{
    public event Action<int> ChangeItemCount;
    public event Action EndItem;

    [SerializeField] private Follow follow;
    [SerializeField] private MainSpawnPoint ballSpawnPoint;
    [SerializeField] private SpawnPoint[] spawnPoints;
    private List<Item> items = new List<Item>();

    private IChangeGameSate changeGameSate;
    private Ball ball;

    public void Initialise(IChangeGameSate changeGameSate)
    {
        this.changeGameSate = changeGameSate;
        foreach (var item in spawnPoints)
        {
            var tmp = Instantiate(PrefabsStore.Instance.ItemPrefabs[1], item.transform.position, Quaternion.identity);
            tmp.actionOnHit += RemoveItem;
            items.Add(tmp);
        }

        follow.actionSetTarget += CameraChangeFollow;

    }

    public void SpawnBall(Ball ballPrefab)
    {
        ball = Instantiate(ballPrefab, ballSpawnPoint.transform.position, Quaternion.identity);
        ball.Initialise(follow, changeGameSate);
    }

    private void CameraChangeFollow(Transform followTarget)
    {
        if(followTarget == ball.transform)
        {
            if(items.Count <= 0)
            {
                EndItem?.Invoke();
            }
        }
    }

    private void RemoveItem(Item item)
    {
        items.Remove(item);
        ChangeItemCount?.Invoke(items.Count);
    }

    int IItemCount.CurrentItemCount()
    {
        return items.Count;
    }

    [ExecuteInEditMode]
    public void FindSpawnPoint()
    {
        spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
    }

}
