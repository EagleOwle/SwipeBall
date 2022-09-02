using System;
using UnityEngine;

public enum GameState
{
    Game,
    Pause
}

public interface IChangeGameSate
{
    event EventHandler<GameState> ChangeGameSate;
}

public class Game : MonoBehaviour, IChangeGameSate
{
    [SerializeField] private ManagerMenu managerMenu;
    [SerializeField] private Environment environment;
    [SerializeField] private AudioSource audioSource;
    public event EventHandler<GameState> ChangeGameSate;

    private void Start()
    {
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

}
