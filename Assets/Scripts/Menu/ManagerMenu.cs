using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour
{
    public Action<GameState> actionChangeGameState;

    [SerializeField] private GameMenu gamePanel;
    [SerializeField] private PauseMenu pausePanel;
    [SerializeField] private WinMenu winPanel;
    [SerializeField] private GameObject loadScreen;

    private ItemHandler itemHandler;

    public void Initialise(ItemHandler itemHandler)
    {
        this.itemHandler = itemHandler;
        gamePanel.Initialise(itemHandler, this);
        pausePanel.Initialise(this);
        winPanel.Initialise(this);
        HidePauseMenu();
        HideWinMenu();

        Invoke(nameof(EndLoad), 2);
    }

    public void ShowPauseMenu()
    {
        if (winPanel.IsShow()) return;
        pausePanel.Show();
        actionChangeGameState.Invoke(GameState.Pause);
    }

    public void ResumeGame()
    {
        if (winPanel.IsShow()) return;
        HidePauseMenu();
        HideWinMenu();
        actionChangeGameState.Invoke(GameState.Game);
    }

    private void HidePauseMenu()
    {
        pausePanel.Hide();
    }

    public void ShowWinMenu()
    {
        HidePauseMenu();
        actionChangeGameState.Invoke(GameState.Pause);
        winPanel.Show();
    }

    private void HideWinMenu()
    {
        winPanel.Hide();
    }

    public void ExitScene()
    {
        SceneManager.LoadScene(0);
    }

    private void EndLoad()
    {
        loadScreen.SetActive(false);
        actionChangeGameState.Invoke(GameState.Game);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
