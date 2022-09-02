using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip tapClip;
    [SerializeField] private AudioSource audioSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(tapClip);
    }
}
