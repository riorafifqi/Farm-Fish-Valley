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

    public bool isEnd = false;
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
            // Lose() only on timer quest script
        }
    }

    private void End()
    {
        finishPanel.SetActive(true);    // activate finish panel
        scoreText.text = scoreManager.GetScore().ToString();  // show final score on panel
        Time.timeScale = 0; // pause game
    }

    private bool CheckCondition()   
    {
        foreach (Quest quest in quests)     // check each quest
        {
            if (!quest.isComplete)      // return false if there is incomplete quest
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
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        InGameMenu.isPaused = false;
        Time.timeScale = 1f;
    }

    private void Win()
    {
        End();

        finishText.text = "Clear";    // set finish text
        winButton.SetActive(true);      // next button
        loseButton.SetActive(false);    // retry button
        finishPanel.GetComponent<WinAnimation>().WinDecorationOpen();

        UnlockLevel();      // unlock next level
    }

    public void Lose()
    {
        End();

        finishText.text = "Failed";     // set finish text
        winButton.SetActive(false);     // next button
        loseButton.SetActive(true);     // retry button
    }

    private void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().name.Contains("Udang"))       // unlock level at udang level
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("udangLevelAt"))
            {
                PlayerPrefs.SetInt("udangLevelAt", nextSceneLoad);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Nila"))   // unlock level at nila level
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("nilaLevelAt"))
            {
                PlayerPrefs.SetInt("nilaLevelAt", nextSceneLoad);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Bandeng"))    // unlock level at bandeng level
        {
            if (nextSceneLoad > PlayerPrefs.GetInt("bandengLevelAt"))
            {
                PlayerPrefs.SetInt("bandengLevelAt", nextSceneLoad);
            }
        }
    }
}