using UnityEngine;
using System.Collections;

public class TransferAnimationEvent : MonoBehaviour
{
    [SerializeField] private TutorialMenu tutorialMenu;

    public void EndAnimation()
    {
        tutorialMenu.EndAnimation();
    }
}
