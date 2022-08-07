using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerQuest : MonoBehaviour
{
    EndGame endGame;
    public int questId;
    //public GameObject questParent;
    public List<Quest> timerQuests;
    public List<Quest> activatedTimerQuests;
    public int questCount = 1;

    public float timeTillActive = 1f;
    public float currentTime = 0f;

    public float timeRemaining = 60f;
    public float timer = 0f;
    public bool isInProgress = false;

    private void Awake()
    {
        endGame = GameObject.Find("GameManager").GetComponent<EndGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < questCount; i++)
        {
            while (activatedTimerQuests.Contains(timerQuests[questId]))
            {
                questId = Random.Range(0, timerQuests.Count - 1);
            }
            activatedTimerQuests.Add(timerQuests[questId]);

        }
        questId = Random.Range(0, timerQuests.Count - 1);
        //timerQuest = questParent.transform.GetChild(questId).GetComponent<Quest>();
    }

    void Update()
    {
        if (currentTime > timeTillActive && !isInProgress)
        {
            InstantiateQuest();
            currentTime = 0f;
        }
        currentTime += Time.deltaTime;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            int timerToInt = (int)timer;

            foreach (Quest activeQuest in activatedTimerQuests)
            {
                activeQuest.gameObject.GetComponentInChildren<Text>().text = timerToInt.ToString() + "s";   
            }
        }

        if (timer <= 0f && isInProgress)
        {
            timer = 0f; // timer off
            foreach (Quest activeQuest in activatedTimerQuests)
            {
                if (!activeQuest.isComplete)
                    endGame.Lose();     // Game End with losing 
            }
        }

        foreach (Quest activeQuest in activatedTimerQuests)
        {
            if (activeQuest.isComplete)
            {
                activeQuest.gameObject.SetActive(false);    // deactivate active quest gameobject
                activatedTimerQuests.Remove(activeQuest);   // remove active quest from list
            }
        }

        if(activatedTimerQuests.Count == 0) // if all complete
        {
            for (int i = 0; i < questCount; i++)
            {
                while (activatedTimerQuests.Contains(timerQuests[questId]))
                {
                    questId = Random.Range(0, timerQuests.Count - 1);
                }
                timerQuests[questId].isComplete = false;
                activatedTimerQuests.Add(timerQuests[questId]);
            }

            timer = 0f; // reset timer
            isInProgress = false;
        }

        /*if (timerQuest.isComplete)
        {
            timerQuest.transform.parent.gameObject.SetActive(false); // deactivate parent
            questParent.transform.GetChild(questId).gameObject.SetActive(false);

            questId = Random.Range(0, questParent.transform.childCount - 1);    // randomize new quest
            timerQuest = questParent.transform.GetChild(questId).GetComponent<Quest>();

            timer = 0f; // reset timer

            isInProgress = false;
            timerQuest.isComplete = false;
        }*/
    }

    void InstantiateQuest()
    {
        foreach (Quest activeQuest in activatedTimerQuests)
            activeQuest.gameObject.SetActive(true);

        // timer start
        timer = timeRemaining;
        isInProgress = true;
    }
}
