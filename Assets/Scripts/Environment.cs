using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IItemCount
{
    event EventHandler<int> ChangeItemCount;
    event EventHandler EndItem;
    int CurrentItemCount();
}

public class Environment : MonoBehaviour, IItemCount
{
    public event EventHandler<int> ChangeItemCount;
    public event EventHandler EndItem;

    [SerializeField] private Follow follow;
    [SerializeField] private MainSpawnPoint ballSpawnPoint;
    [SerializeField] private SpawnPoint[] spawnPoints;
    private List<Item> items = new List<Item>();

    private Ball ball;

    public void Initialise(IChangeGameSate changeGameSate)
    {
        foreach (var item in spawnPoints)
        {
            var tmp = Instantiate(PrefabsStore.Instance.ItemPrefabs[1], item.transform.position, Quaternion.identity);
            tmp.actionOnHit += RemoveItem;
            items.Add(tmp);
        }

        changeGameSate.ChangeGameSate += ChangeGameSate;

        follow.actionSetTarget += CameraChangeFollow;

    }

    public void SpawnBall(Ball ballPrefab)
    {
        ball = Instantiate(ballPrefab, ballSpawnPoint.transform.position, Quaternion.identity);
        ball.Initialise(follow);
    }

    private void CameraChangeFollow(Transform followTarget)
    {
        if(followTarget == ball.transform)
        {
            if(items.Count<=0)
            {
                EndItem.Invoke(this, null);
            }
        }
    }

    private void ChangeGameSate(object sender, GameState state)
    {
        if (ball == null) return;

        switch (state)
        {
            case GameState.Game:
                ball.OnPushable = true;
                break;
            case GameState.Pause:
                ball.OnPushable = false;
                break;
        }
    }

    private void RemoveItem(Item item)
    {
        items.Remove(item);
        ChangeItemCount?.Invoke(this, items.Count);
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
