using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MapManager
{
    private int score = 0;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = score.ToString();  // update score UI for testing
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public int GetScore()
    {
        return score;
    }

}
