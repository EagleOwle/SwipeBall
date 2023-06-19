using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicValumeSlider;
    [SerializeField] private Slider soundValumeSlider;

    public override void Initialise(ManagerMenu managerMenu)
    {
        base.Initialise(managerMenu);
        resumeButton.onClick.AddListener(OnButtonResum);
        exitButton.onClick.AddListener(OnExitButton);
        musicValumeSlider.onValueChanged.AddListener(MusicSliderOnVolumeChenge);
        soundValumeSlider.onValueChanged.AddListener(SoundSliderOnVolumeChenge);
    }

    private void OnEnable()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicValumeSlider.value = musicVolume;
    }

    private void MusicSliderOnVolumeChenge(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        SoundController.Instance.SetMusicVolume(value);
    }
    
    private void SoundSliderOnVolumeChenge(float value)
    {
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    private void OnExitButton()
    {
        managerMenu.ExitScene();
    }

    private void OnButtonResum()
    {
        managerMenu.ResumeGame();
    }

}
