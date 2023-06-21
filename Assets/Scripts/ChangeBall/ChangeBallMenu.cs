using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBallMenu : MonoBehaviour
{
    public event Action<int> EventBallIsChanged;

    [SerializeField] private CarouselPrewievPlace carousel;
    [SerializeField] private Button startButton;
    [SerializeField] private Text blockedText;

    public void Initialise(IChangeGameState changeGameSate)
    {
        startButton.onClick.AddListener(OnButtonStart);
        startButton.gameObject.SetActive(true);
        changeGameSate.EventOnChangeGameState += ChangeGameSate;
        carousel.actionSetCurrentPoint += SetCurrentPoint;
        SetCurrentPoint(carousel.CurrentPoint.index);

        OnButtonStart();
        //Invoke(nameof(OnButtonStart), 3);

    }

    private void SetCurrentPoint(int index)
    {
        Acces acces = PrefabsStore.Instance.balls[index].acces;

        switch (acces)
        {
            case Acces.Free:
                blockedText.gameObject.SetActive(false);
                startButton.gameObject.SetActive(true);
                break;
            case Acces.Blocked:
                blockedText.gameObject.SetActive(true);
                startButton.gameObject.SetActive(false);
                break;
            case Acces.Available:
                blockedText.gameObject.SetActive(false);
                startButton.gameObject.SetActive(true);
                break;
        }
    }

    private void OnButtonStart()
    {
        carousel.Hide();
        EventBallIsChanged?.Invoke(carousel.CurrentPoint.index);
    }

    private void ChangeGameSate(GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                startButton.gameObject.SetActive(true);
                SetCurrentPoint(carousel.CurrentPoint.index);
                break;
            case GameState.Pause:
                startButton.gameObject.SetActive(false);
                blockedText.gameObject.SetActive(false);
                break;
        }
    }
}
