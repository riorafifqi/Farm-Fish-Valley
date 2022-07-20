using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public string taskName;
    public int taskToCompleteProgress;
    public int taskCurrentProgress = 0;
    public Color completeColor;
    public bool isComplete;

    public Text taskNameUI;
    public Text taskConditionUI;

    public QuestCompleteCondition questCondition;
    public QuizManager quiz;

    private void Start()
    {
        taskNameUI.text = taskName;
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";
    }

    // Update is called once per frame
    void Update()
    {
        taskNameUI.text = taskName;
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";

        if (questCondition.Condition())
        {
            if(!isComplete)
                taskCurrentProgress++;
        }

        if (taskCurrentProgress == taskToCompleteProgress)
        {
            if (!quiz)  // if there's no quiz
                isComplete = true;
            else
            {
                quiz.transform.gameObject.SetActive(true);
                isComplete = true;
                if (quiz.result)
                    quiz.transform.gameObject.SetActive(false);
            }
        }

        if (isComplete)
        {
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }

    }
}
