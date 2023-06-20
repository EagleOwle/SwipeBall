using System;
using UnityEngine;

public enum GameState
{
    Game,
    Pause,
}

public interface IChangeGameSate
{
    event Action<GameState> ChangeGameSate;
}

public class Game : MonoBehaviour, IChangeGameSate
{
    public event Action<GameState> ChangeGameSate;

    [SerializeField] private MazeSpawner mazeSpawner;
    [SerializeField] private ItemHandler itemHandler;
    [SerializeField] private ChangeBallMenu changeBallMenu;
    [SerializeField] private ManagerMenu managerMenu;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FollowTargetChanger followTargetChanger;

    private Ball ball;

    private void Start()
    {
        Application.targetFrameRate = 30;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        audioSource.volume = musicVolume;

        mazeSpawner.Spawn();
        itemHandler.GenerateItem();

        followTargetChanger.EventFollowSetTarget += FollowTargetChanger_EventFollowSetTarget;

        changeBallMenu.EventBallIsChanged += ChangeBallMenu_EventBallIsChanged;
        changeBallMenu.Initialise(this as IChangeGameSate);

        managerMenu.actionChangeGameState += OnChangeGameState;
        managerMenu.Initialise(itemHandler);
    }

    private void FollowTargetChanger_EventFollowSetTarget(Transform followTarget)
    {
        if (followTarget == ball.transform)
        {
            if (itemHandler.CurrentItemCount() <= 0)
            {
                managerMenu.ShowWinMenu();
            }
        }
    }

    private void ChangeBallMenu_EventBallIsChanged(int value)
    {
        changeBallMenu.EventBallIsChanged -= ChangeBallMenu_EventBallIsChanged;
        PrefabsStore.Instance.SetCurrentBall(value);
        StartGame();
    }

    private void OnChangeGameState(GameState state)
    {
        ChangeGameSate?.Invoke(state);
    }

    private void StartGame()
    {
        ball = GameObject.FindObjectOfType<PlayerSpawnPoint>().SpawnPlayer();
        ball.Initialise(followTargetChanger, this as IChangeGameSate);
    }
}
