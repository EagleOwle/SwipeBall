using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BoxOpenEvent : AnimatorEventListener
{
    public UnityEvent eventBoxIsOpen;

    public override void AnimationEvent(AnimationEvent animationEvent)
    {
        if(animationEvent.stringParameter == "BoxOpen")
        {
            eventBoxIsOpen.Invoke();
        }
    }
}