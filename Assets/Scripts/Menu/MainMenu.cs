using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitApplication);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    private void StartGame()
    {
       SceneManager.LoadScene("Game");
    }

    private void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#elif UNITY_IOS
         Application.Quit();
#else
         Application.Quit();
#endif
    }

}
