using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMenuAnimation : MonoBehaviour
{
    public void SwipeLeftIn()
    {
        transform.localPosition = new Vector2(Screen.width, 0);
        transform.gameObject.SetActive(true);
        transform.LeanMoveLocalX(0, 0.5f);
    }

    public void SwipeLeftOut()
    {
        transform.localPosition = new Vector2(0, 0);
        transform.LeanMoveLocalX(-Screen.width, 0.5f).setOnComplete(OnOutComplete);
    }

    public void SwipeRightIn()
    {
        transform.localPosition = new Vector2(-Screen.width, 0);
        transform.gameObject.SetActive(true);
        transform.LeanMoveLocalX(0, 0.5f);
    }

    public void SwipeRightOut()
    {
        transform.localPosition = new Vector2(0, 0);
        transform.LeanMoveLocalX(Screen.width, 0.5f).setOnComplete(OnOutComplete);
    }

    public void OnOutComplete()
    {
        gameObject.SetActive(false);
    }
}
