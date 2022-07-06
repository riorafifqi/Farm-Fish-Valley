using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseInteract : MonoBehaviour
{
    public GameObject finishPanel;
    public ScoreManager scoreManager;
    public Text scoreText;

    private void OnMouseDown()
    {
        finishPanel.SetActive(true);
        scoreText.text = "Your Score : " + scoreManager.GetScore().ToString();
    }
}
