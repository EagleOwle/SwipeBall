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

    public void Initialise(IItemCount itemCount)
    {
        gamePanel.Initialise(itemCount, this);
        pausePanel.Initialise(this);
        winPanel.Initialise(this);
        HidePauseMenu();
        HideWinMenu();

        //itemCount.ChangeItemCount += ChangeItemCount;
        itemCount.EndItem += EndItem;
        actionChangeGameState.Invoke(GameState.Game);
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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
