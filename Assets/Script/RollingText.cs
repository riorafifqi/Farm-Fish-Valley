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
        StartCoroutine(Waiter());
        Time.timeScale = 0;
        textRect.sizeDelta = new Vector2(0, 1);
    }

    void Update()
    {
        if (textRect.anchoredPosition.y >= textRect.sizeDelta.y)
        {
            scrollAnimator.SetBool("isPlaying", false);
            scrollAnimator.speed = 0;
            xButton.SetActive(true);
        }
    }

    public IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(5f);

        scrollAnimator.SetBool("isPlaying", true);
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
