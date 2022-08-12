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
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";        // update progress to UI

        if(questCondition)      // if QuestCompleteCondition script exist
        {
            if (!timerQuest.isInProgress)       // if timer quest is not in progress
                if (questCondition.Condition())     // if quest condition scriptable object return true
                {
                    if (!isComplete)
                        taskCurrentProgress++;      // update progress
                }

        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if complete
        {
            if (soalQuiz && !isComplete)  // if there's a quiz
            {
                quiz.soalQuiz = soalQuiz;       // assign quiz's question

                //quiz.transform.gameObject.SetActive(true);
                quiz.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);   // open quiz

                if (quiz.result)    // if the answer is right
                {
                    isComplete = true;      // complete the task
                    quiz.gameObject.GetComponent<Animator>().SetBool("IsOpen", false);  // close quiz
                    
                    quiz.result = false;    // toggle result back to false to prevent loop
                }
            }
            else if (description.Length != 0)       // if there is description after task
            {
                descriptionText.text = description;     // assign description to UI
                descriptionAnim.SetBool("IsOpen", true);    // open desc panel
                if(descriptionHandler.isFinish)     // if player is finish reading by pressing close button
                {
                    descriptionAnim.SetBool("IsOpen", false);   // close desc panel
                    isComplete = true;      // complete task
                }
            }
            else  // no desc or quiz
            {
                isComplete = true;  // complete task    
            }
        }

        if (isComplete)     // if complete
        {
            // change task color to complete color
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }

    }

    private void OnEnable()
    {
        // set color and progress to default when enabled
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;
    }
}
