using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelAnimation : MonoBehaviour
{
    public Transform panel;
    public CanvasGroup background;

    public void OnEnable()
    {
        background.alpha = 0f;
        background.LeanAlpha(1, 0f).setIgnoreTimeScale(true);

        panel.localPosition = new Vector2(0, -Screen.height);
        panel.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true).delay = 0.1f;
    }

    public void Close()
    {
        background.LeanAlpha(0, 0.5f).setIgnoreTimeScale(true);
        panel.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInOutExpo().setIgnoreTimeScale(true).setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
