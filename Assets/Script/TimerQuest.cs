using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerQuest : MonoBehaviour
{
    EndGame endGame;
    public int questId;     // needed to randomize random task
    //public GameObject questParent;
    public List<Quest> timerQuests;     // contain all timer/random task
    public List<Quest> activatedTimerQuests;    // current active quest
    public int questCount = 1;      // amount quest going to activate

    public float timeTillActive = 1f;       // duration needed before task is starting
    public float currentTime = 0f;      // current duration

    public float timeRemaining = 60f;       // remaining time for player to complete the task
    public float timer = 0f;            // current remaining time
    public bool isInProgress = false;   // random task tracker

    private void Awake()
    {
        endGame = GameObject.Find("GameManager").GetComponent<EndGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < questCount; i++)        // loop to activate task based on amount of quest
        {
            while (activatedTimerQuests.Contains(timerQuests[questId]))     // loop while system generate same task
            {
                questId = Random.Range(0, timerQuests.Count - 1);       // generate task id
            }
            activatedTimerQuests.Add(timerQuests[questId]);     // add task to list of "going to be activated" task

        }
        questId = Random.Range(0, timerQuests.Count - 1);       // generate task id to prevent system generate same task on the next phase
        //timerQuest = questParent.transform.GetChild(questId).GetComponent<Quest>();
    }

    void Update()
    {
        if (currentTime > timeTillActive && !isInProgress)      // if duration is up and no random task is in progress
        {
            InstantiateQuest();     // instantiate task
            currentTime = 0f;       // set duration back to 0
        }
        currentTime += Time.deltaTime;      // increment duration

        if (timer > 0)      // if there is time remaining
        {
            timer -= Time.deltaTime;        // decrement timer
            int timerToInt = (int)timer;        // convert timer string to int

            foreach (Quest activeQuest in activatedTimerQuests)     // update each random task timer UI
            {
                activeQuest.gameObject.GetComponentInChildren<Text>().text = timerToInt.ToString() + "s";   
            }
        }

        if (timer <= 0f && isInProgress)    // if time is up but task isn't complete
        {
            timer = 0f; // timer off       
            foreach (Quest activeQuest in activatedTimerQuests)
            {
                if (!activeQuest.isComplete)        // if there is task incomplete
                    endGame.Lose();     // Game End with losing 
            }
        }

        foreach (Quest activeQuest in activatedTimerQuests)
        {
            if (activeQuest.isComplete)     // if task in the list is complete
            {
                activeQuest.gameObject.SetActive(false);    // deactivate active random quest gameobject
                activatedTimerQuests.Remove(activeQuest);   // remove active quest from list
            }
        }

        if(activatedTimerQuests.Count == 0) // if all complete
        {
            for (int i = 0; i < questCount; i++)        // regenerate new task like in Start() function
            {
                while (activatedTimerQuests.Contains(timerQuests[questId]))
                {
                    questId = Random.Range(0, timerQuests.Count - 1);
                }
                timerQuests[questId].isComplete = false;        // set isComplete to default
                activatedTimerQuests.Add(timerQuests[questId]);
            }

            timer = 0f; // reset timer
            isInProgress = false;
        }
    }

    void InstantiateQuest()
    {
        foreach (Quest activeQuest in activatedTimerQuests)     // set active all "going to be activated" task in the list
            activeQuest.gameObject.SetActive(true);

        // timer start
        timer = timeRemaining;
        isInProgress = true;    // set inProgress to true
    }
}
