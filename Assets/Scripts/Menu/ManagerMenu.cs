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

    public void Initialise(IItemCount itemCount)
    {
        gamePanel.Initialise(itemCount, this);
        pausePanel.Initialise(this);
        winPanel.Initialise(this);
        HidePauseMenu();
        HideWinMenu();

        itemCount.EndItem += EndItem;
        Invoke(nameof(EndLoad), 2);
    }

    private void EndItem(object sender, EventArgs e)
    {
        HidePauseMenu();
        ShowWinMenu();
    }

    public void ShowPauseMenu()
    {
        pausePanel.Show();
        actionChangeGameState.Invoke(GameState.Pause);
    }

    public void ResumeGame()
    {
        HidePauseMenu();
        HideWinMenu();
        actionChangeGameState.Invoke(GameState.Game);
    }

    private void HidePauseMenu()
    {
        pausePanel.Hide();
    }

    private void ShowWinMenu()
    {
        winPanel.Show();
        actionChangeGameState.Invoke(GameState.Pause);
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
