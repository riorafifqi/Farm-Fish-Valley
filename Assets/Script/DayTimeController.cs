using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f;  // 15 minutes
    int oldPhase = 0;

    float time;
    [SerializeField] float timeScale = 60f;
    [SerializeField] float startTime = 28800f; // 8.00 AM in seconds

    [SerializeField] Color nightColor;
    [SerializeField] AnimationCurve timeCurve; 
    [SerializeField] Color dayColor;

    [SerializeField] Text timeText;
    [SerializeField] Text dayText;
    [SerializeField] Light2D globalLight;

    List<TimeAgent> agents;

    void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    private int days;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    void Start()
    {
        time = startTime;

        int hh = (int)Hours;
        int mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        TimeCalculation();

        DayNightLight();

        if (time > secondsInDay)
        {
            NextDay();
        }

        TimeAgentsInvoke();
    }

    private void TimeAgentsInvoke()
    {
        int currentPhase = (int)(time / phaseLength);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }

        
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }

    private void TimeCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;

        dayText.text = "Day " + days;
        if (mm % 10 == 0)
            timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void DayNightLight()
    {
        /*float v = timeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globalLight.color = c;*/
    }
}
