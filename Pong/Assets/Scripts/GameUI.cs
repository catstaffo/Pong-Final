using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreTextPlayerOne, scoreTextPlayerTwo;
    public GameObject menuObject;
    public TextMeshProUGUI playModeButtonText;
    public TextMeshProUGUI winText;
   
    public System.Action onStartGame;

    private void Awkake()
    {
        AdjustPlayModeButtonText();
    }

    public void UpdateScores(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayerOne.SetScore(scorePlayer1);
        scoreTextPlayerTwo.SetScore(scorePlayer2);

    }

    public void HighlightScore (int id)
    {
        if (id == 1)
            scoreTextPlayerOne.Highlight();
        else
            scoreTextPlayerTwo.Highlight();
    }

    public void OnStartGameButtonClicked()
    {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId)
    {
        menuObject.SetActive(true);
        winText.text = "Player " + winnerId + " wins!";
    }

    public void OnSwitchPlayModeButtonClicked()
    {
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }


    private void AdjustPlayModeButtonText()
    {

        switch (GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                playModeButtonText.text = "Player vs Player";
                break;

            case GameManager.PlayMode.PlayerVsAi:
                 playModeButtonText.text = "Player vs Ai";
                 break;
        }

        
    }
}
