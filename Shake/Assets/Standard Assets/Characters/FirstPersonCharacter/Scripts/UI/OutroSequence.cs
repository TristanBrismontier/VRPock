using UnityEngine;
using System.Collections;

public class OutroSequence : MonoBehaviour 
{
    public CanvasGroup Darkenator;
    public CanvasGroup GameOver;
    public CanvasGroup GameWon;

    public Animator SequenceAnimator;

    public string WinState = "GameWin";
    public string LoseState = "GameOver";

    public float Delay = 3.0f;

    bool gameOver = false;

    public void PlayGameEnd(bool gameWon)
    {
        StartCoroutine(PlayAfterDelay(gameWon));
    }

    IEnumerator PlayAfterDelay(bool gameWon)
    {
        yield return new WaitForSeconds(Delay);

        if(gameWon)
            SequenceAnimator.Play(WinState);
        else
            SequenceAnimator.Play(LoseState);
    }

    public void OutroComplete()
    {
        gameOver = true;
    }

    void Update()
    {
        if(gameOver)
            Application.LoadLevel(0);
    }
}
