using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [Space()]
    [SerializeField] private Button musicButton;
    [SerializeField] private Outline musicButtonOutline;
    [SerializeField] private Text musicButtonText;
    [Space()]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;
    [Space()]
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        pauseButton.onClick.AddListener(ShowPauseMenu);
        resumeButton.onClick.AddListener(HidePauseMenu);
        exitButton.onClick.AddListener(ExitGame);
        musicButton.onClick.AddListener(Music);
        HidePauseMenu();
        Music();
    }

    private void Music()
    {
        if(audioSource.volume == 0)
        {
            audioSource.volume = 1;
            musicButtonOutline.enabled = true;
            musicButtonText.text = "Music On";
        }
        else
        {
            audioSource.volume = 0;
            musicButtonOutline.enabled = false;
            musicButtonText.text = "Music Off";
        }
    }

    private void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pausePanel.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

}
