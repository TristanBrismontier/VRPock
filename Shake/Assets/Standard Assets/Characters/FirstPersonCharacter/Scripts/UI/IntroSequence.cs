using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour 
{
    public CanvasGroup Darkenator;
    public CanvasGroup Title;
    public CanvasGroup HowToPlay;

    public Animator SequenceAnimator;

    public void IntroStarted()
    {
        if(GameManager.IntroStartedEvent != null)
            GameManager.IntroStartedEvent.Invoke();
    }

    public void IntroComplete()
    {
        if(GameManager.IntroFinishedEvent != null)
            GameManager.IntroFinishedEvent.Invoke();
    }
}