using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelAnimation : MonoBehaviour
{
    public void Open()
    {
        transform.localPosition = new Vector2(0, Screen.height);
        transform.LeanMoveLocalY(0f, 0.5f).setEaseInBounce().setIgnoreTimeScale(true);
    }

    public void Close()
    {
        transform.LeanMoveLocalY(Screen.height, 0.5f).setEaseInBack().setIgnoreTimeScale(true);
    }

}
