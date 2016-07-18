using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{
    #region Singleton

    // Bad implementation don't call until after Awake
    private static UIManager s_instance;
    public static UIManager Instance
    {
        get { return s_instance; }
    }

    #endregion // Singleton

    public IntroSequence Intro;
    public OutroSequence Outro;

    void Awake()
    {
        s_instance = this;

        GameManager.GameEndedEvent += GameOver;
    }

    void OnDisable()
    {
        GameManager.GameEndedEvent -= GameOver;
    }

    void GameOver(bool won)
    {
        Outro.PlayGameEnd(won);
    }
}
