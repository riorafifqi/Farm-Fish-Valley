using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void Open()
    {
        transform.LeanScale(Vector2.one, 0.8f).setIgnoreTimeScale(true);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 0.8f).setEaseInBack().setIgnoreTimeScale(true);
    }
}
