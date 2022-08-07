using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public string taskName;
    public int taskToCompleteProgress;
    public int taskCurrentProgress = 0;
    public Color completeColor;
    public bool isComplete = false;

    public Text taskNameUI;
    public Text taskConditionUI;

    public QuestCompleteCondition questCondition;
    public QuizManager quiz;
    public SoalQuiz soalQuiz;
    public TimerQuest timerQuest;

    [TextArea] public string description;
    public Animator descriptionAnim;
    public TMP_Text descriptionText;
    public DescriptionHandler descriptionHandler;

    private void Awake()
    {
        quiz = GameObject.Find("Quiz").GetComponent<QuizManager>();
        timerQuest = GameObject.Find("Quest").GetComponent<TimerQuest>();

        descriptionAnim = GameObject.Find("DescriptionText").GetComponent<Animator>();
        descriptionText = GameObject.Find("DescriptionText").GetComponentInChildren<TMP_Text>();
        descriptionHandler = GameObject.Find("DescriptionText").GetComponent<DescriptionHandler>();
    }

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

        if(questCondition)
        {
            if (!timerQuest.isInProgress)
                if (questCondition.Condition())
                {
                    if (!isComplete)
                        taskCurrentProgress++;
                }

        }

        if (taskCurrentProgress == taskToCompleteProgress)
        {
            if (soalQuiz && !isComplete)  // if there's a quiz
            {
                quiz.soalQuiz = soalQuiz;

                //quiz.transform.gameObject.SetActive(true);
                quiz.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);   // open quiz

                if (quiz.result)
                {
                    isComplete = true;
                    quiz.gameObject.GetComponent<Animator>().SetBool("IsOpen", false);  // close quiz
                    //quiz.transform.gameObject.SetActive(false);
                    quiz.result = false;
                }
            }
            else if (description.Length != 0)
            {
                descriptionText.text = description;
                descriptionAnim.SetBool("IsOpen", true);
                if(descriptionHandler.isFinish)
                {
                    descriptionAnim.SetBool("IsOpen", false);
                    isComplete = true;
                }
            }
            else
            {
                isComplete = true;
            }
        }

        if (isComplete)
        {
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }

    }

    private void OnEnable()
    {
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;
    }
}
