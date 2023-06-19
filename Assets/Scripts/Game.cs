using System;
using UnityEngine;

public enum GameState
{
    Game,
    Pause,
}

public interface IChangeGameSate
{
    event EventHandler<GameState> ChangeGameSate;
}

public interface IStartGame
{
    void StartGame();
}

public class Game : MonoBehaviour, IChangeGameSate, IStartGame
{
    public event EventHandler<GameState> ChangeGameSate;

    [SerializeField] private CarouselPrewievPlace carousel;
    [SerializeField] private ChangeBallMenu changeBallMenu;
    [SerializeField] private ManagerMenu managerMenu;
    [SerializeField] private Environment environment;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        //carousel.Initialise(PrefabsStore.Instance.balls.Count);
        changeBallMenu.Initialise(this, this);

        environment.Initialise(this);

        managerMenu.actionChangeGameState += OnChangeGameState;
        managerMenu.Initialise(environment);

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        audioSource.volume = musicVolume;
    }

    private void OnChangeGameState(GameState state)
    {
        ChangeGameSate.Invoke(this, state);
    }

    void IStartGame.StartGame()
    {
        carousel.Hide();
        environment.SpawnBall(PrefabsStore.Instance.CurrentBallPrefab);
    }
}
