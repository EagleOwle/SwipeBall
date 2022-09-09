﻿using UnityEngine;
using System.Collections;

public class TutorialMenu : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void EndAnimation()
    {
       Invoke(nameof(Hide),1);
    }
}
