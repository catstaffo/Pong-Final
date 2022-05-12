using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;
    public GameAudio gameAudio;
    public int scorePlayer1, scorePlayer2;
    public int maxScore = 3;
    public GameObject ball;
    public PlayMode playMode;
    
    public System.Action onReset;


    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAi
    }


    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }

    private void OnDestroy()
    {
        gameUI.onStartGame -= OnStartGame;
    }

    public void OnScoreZoneReached(int id)
    {
        

        if (id == 1)
            scorePlayer1++;
        if (id == 2)
            scorePlayer2++;
        
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    private void CheckWin()
    {
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if (winnerId != 0)
        {
           gameUI.OnGameEnds(winnerId);
           gameAudio.PlayWinSound();
        }
        else
        {
            onReset?.Invoke();
            gameAudio.PlayScoreSound();
        }
    }

    private void OnStartGame()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
    }

    public void SwitchPlayMode()
    {
        switch (playMode)
        {
            case PlayMode.PlayerVsPlayer:
            playMode = PlayMode.PlayerVsAi;
            break;

            case PlayMode.PlayerVsAi:
            playMode = PlayMode.PlayerVsPlayer;
            break;
        }
    }

    public bool IsPlayer2Ai()
    {
        return playMode == PlayMode.PlayerVsAi;
    }

    
}
