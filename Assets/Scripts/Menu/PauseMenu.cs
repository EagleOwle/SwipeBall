using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider musicValumeSlider;

    public override void Initialise(ManagerMenu managerMenu)
    {
        base.Initialise(managerMenu);
        resumeButton.onClick.AddListener(OnButtonResum);
        exitButton.onClick.AddListener(OnExitButton);
        musicValumeSlider.onValueChanged.AddListener(OnVolumeChenge);
    }

    private void OnEnable()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicValumeSlider.value = musicVolume;
    }

    private void OnVolumeChenge(float value)
    {
        //audioSource.volume = value;
        //PlayerPrefs.SetFloat("MusicVolume", value);
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
