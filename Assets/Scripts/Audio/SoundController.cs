using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    public static SoundController Instance
    {
        get
        {

            if(instance == null)
            {
                instance = GameObject.FindFirstObjectByType<SoundController>();
            }

            return instance;
        }
    }

    [SerializeField] private AudioSource audioSource;

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioSource.volume = volume;
    }

    public AudioSource PlayClipAtPosition(AudioClip clip, Vector3 position)
    {
        GameObject go = new GameObject("One Shot Audio");
        go.transform.position = position;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = PlayerPrefs.GetFloat("SoundVolume");
        source.Play();
        Destroy(go, source.clip.length);
        return source;
    }

}
