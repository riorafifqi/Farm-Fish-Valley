using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerQuest : MonoBehaviour
{
    EndGame endGame;
    public int questId;
    public GameObject questParent;
    public Quest timerQuest;

    public float timeTillActive = 1f;
    public float currentTime = 0f;

    public float timeRemaining = 60f;
    public float timer = 0f;
    public Text timerUI;
    public bool isInProgress = false;

    private void Awake()
    {
        endGame = GameObject.Find("GameManager").GetComponent<EndGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //timeTillActive = Random.Range(minTime, maxTime);
        questId = Random.Range(0, questParent.transform.childCount - 1);
        timerQuest = questParent.transform.GetChild(questId).GetComponent<Quest>();
    }

    void Update()
    {
        if (currentTime > timeTillActive && !isInProgress)
        {

            InstantiateQuest();
            currentTime = 0f;
        }
        else if (!isInProgress)
            currentTime += Time.deltaTime;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            int timerToInt = (int)timer;
            timerUI.text = timerToInt.ToString() + "s";

            if (timer <= 0f)
            {
                timer = 0f; // timer off
                if (!timerQuest.isComplete)
                    endGame.Lose();     // Game End with losing 
            }
        }

        if (timerQuest.isComplete)
        {
            timerQuest.transform.parent.gameObject.SetActive(false); // deactivate parent
            questParent.transform.GetChild(questId).gameObject.SetActive(false);

            questId = Random.Range(0, questParent.transform.childCount - 1);
            timerQuest = questParent.transform.GetChild(questId).GetComponent<Quest>();
            timer = 0f;
            isInProgress = false;
        }
    }

    void InstantiateQuest()
    {
        questParent.SetActive(true); // activate parent
        questParent.transform.GetChild(questId).gameObject.SetActive(true);

        // timer start
        timer = timeRemaining;
        isInProgress = true;
    }
}
