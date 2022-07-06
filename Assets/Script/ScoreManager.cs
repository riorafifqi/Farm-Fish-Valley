using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MapManager
{
    private int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log("Current Score ; " + score);
    }

    public int GetScore()
    {
        return score;
    }

}
