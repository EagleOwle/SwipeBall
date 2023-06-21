using System;
using UnityEngine;

public class Game : MonoBehaviour, IChangeGameState
{
    public event Action<GameState> EventOnChangeGameState;

    [SerializeField] private MazeSpawner mazeSpawner;
    [SerializeField] private ItemHandler itemHandler;
    [SerializeField] private ChangeBallMenu changeBallMenu;
    [SerializeField] private ManagerMenu managerMenu;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FollowTargetChanger followTargetChanger;
    [SerializeField] private GameStateHandler swipeGameStateHandler;

    private Ball ball;

    private void Start()
    {
        Application.targetFrameRate = 30;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        audioSource.volume = musicVolume;

        mazeSpawner.Spawn();
        itemHandler.GenerateItem();

        swipeGameStateHandler.Initialise(this as IChangeGameState);

        followTargetChanger.EventFollowSetTarget += FollowTargetChanger_EventFollowSetTarget;

        changeBallMenu.EventBallIsChanged += ChangeBallMenu_EventBallIsChanged;
        changeBallMenu.Initialise(this as IChangeGameState);

        managerMenu.EventOnChangeGameMenu += ManagerMenu_EventOnChangeGameMenu;
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

    private void ManagerMenu_EventOnChangeGameMenu(TypeMenu value)
    {
        switch (value)
        {
            case TypeMenu.GameMenu:
                EventOnChangeGameState?.Invoke(GameState.Game);
                break;
            case TypeMenu.PauseMenu:
                EventOnChangeGameState?.Invoke(GameState.Pause);
                break;
        }
    }

    private void StartGame()
    {
        ball = GameObject.FindObjectOfType<PlayerSpawnPoint>().SpawnPlayer();
        ball.Initialise(followTargetChanger, this as IChangeGameState);
    }
}
