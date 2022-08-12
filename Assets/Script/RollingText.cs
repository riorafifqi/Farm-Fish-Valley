using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class RollingText : MonoBehaviour
{
    public Animator scrollAnimator;
    public RectTransform textRect;
    public GameObject xButton;

    void Start()
    {
        StartCoroutine(Waiter());       // wait before start scrolling
        Time.timeScale = 0;     // pause game
        textRect.sizeDelta = new Vector2(0, 1);     // set size delta to default
    }

    void Update()
    {
        if (textRect.anchoredPosition.y >= textRect.sizeDelta.y)        // if text rolled to the bottom
        {
            scrollAnimator.SetBool("isPlaying", false);     // stop scrolling
            scrollAnimator.speed = 0;           // stop scrolling
            xButton.SetActive(true);        // activate close button
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(5f);    

        scrollAnimator.SetBool("isPlaying", true);
        // change speed in Animator in Text GameObject under ScrollRect to set scrolling text
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<Animator>().SetBool("IsOpen", false);
    }
}
