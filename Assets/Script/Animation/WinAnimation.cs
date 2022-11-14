using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinAnimation : MonoBehaviour
{
    public Transform panel;
    public Transform winDecor;
    public CanvasGroup background;

    public void OnEnable()
    {
        background.alpha = 0f;
        background.LeanAlpha(1, 0f).setIgnoreTimeScale(true);

        panel.LeanScale(Vector3.one, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true).delay = 0.1f;
        //panel.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true).delay = 0.1f;
    }

    public void WinDecorationOpen()
    {
        winDecor.LeanScale(Vector3.one, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true).delay = 0.5f;
    }
}
