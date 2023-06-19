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

    [SerializeField] private ChangeBallMenu changeBallMenu;
    [SerializeField] private ManagerMenu managerMenu;
    [SerializeField] private Environment environment;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        Application.targetFrameRate = 30;

        environment.Initialise(this as IChangeGameSate);

        changeBallMenu.EventBallIsChanged += ChangeBallMenu_EventBallIsChanged;
        changeBallMenu.Initialise(this as IChangeGameSate);

        managerMenu.actionChangeGameState += OnChangeGameState;
        managerMenu.Initialise(environment);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        audioSource.volume = musicVolume;
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
        environment.SpawnBall(PrefabsStore.Instance.CurrentBallPrefab);
    }
}
