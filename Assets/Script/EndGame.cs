using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public int tilesCount;
    public GameObject finishPanel;
    public ScoreManager scoreManager;
    public Text scoreText;
    public Text finishText;

    public GameObject winButton;
    public GameObject loseButton;

    [SerializeField] CropsManager cropsManager;
    int targetScore = 200;
    public Quest[] quests;

    bool isEnd = false;
    public int nextSceneLoad;

    private void Start()
    {
        quests = GameObject.Find("QuestList").GetComponentsInChildren<Quest>();
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void Update()
    {
        isEnd = false;
        //if (cropsManager.crops.Count == tilesCount)
        isEnd = CheckCondition();

        if (isEnd)
        {
            //End();
            Win();
        }
    }

    private void End()
    {
        finishPanel.SetActive(true);
        scoreText.text = "Your Score : " + scoreManager.GetScore().ToString();
        Time.timeScale = 0;
    }

    private bool CheckCondition()
    {
        /*foreach (CropTile cropTile in cropsManager.crops.Values)
        {
            if(!cropTile.growFully)
            {
                return false;
            }
        }*/

        foreach (Quest quest in quests)
        {
            if (!quest.isComplete)
                return false;
        }

        return true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneLoad);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InGameMenu.isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        InGameMenu.isPaused = false;
    }

    private void Win()
    {
        End();

        finishText.text = "Stage Clear";
        winButton.SetActive(true);
        loseButton.SetActive(false);

        UnlockLevel();
    }

    public void Lose()
    {
        End();

        finishText.text = "You Failed";
        winButton.SetActive(false);
        loseButton.SetActive(true);
    }

    private void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().name.Contains("Udang"))
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("udangLevelAt"))
            {
                PlayerPrefs.SetInt("udangLevelAt", nextSceneLoad);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Nila"))
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("nilaLevelAt"))
            {
                PlayerPrefs.SetInt("nilaLevelAt", nextSceneLoad);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Bandeng"))
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("bandengLevelAt"))
            {
                PlayerPrefs.SetInt("bandengLevelAt", nextSceneLoad);
            }
        }
    }
}