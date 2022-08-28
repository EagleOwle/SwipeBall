using System;
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
    [SerializeField] private Slider musicValumeSlider;

    [Space()]
    [SerializeField] private Text itemCountText;

    [Space()]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    [Space()]
    [SerializeField] private AudioSource audioSource;

    public void Initialise(Action<int> actionChangeItemCount)
    {
        pauseButton.onClick.AddListener(ShowPauseMenu);
        resumeButton.onClick.AddListener(HidePauseMenu);
        exitButton.onClick.AddListener(ExitGame);
        musicButton.onClick.AddListener(Music);
        musicValumeSlider.onValueChanged.AddListener(OnVolumeChenge);
        actionChangeItemCount += OnChangeSceneItemCount;
        itemCountText.text = 0.ToString();
        HidePauseMenu();
        //Music();
    }
    
    private void OnChangeSceneItemCount(int value)
    {
        itemCountText.text = value.ToString();
    }

    private void OnEnable()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicValumeSlider.value = musicVolume;
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

    private void OnVolumeChenge(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
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
