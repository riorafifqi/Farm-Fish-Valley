using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void Select()
    {
        transform.LeanScale(new Vector2(1.05f, 1.05f), 0.5f);
    }

    public void Deselect()
    {
        transform.LeanScale(Vector2.one, 0.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deselect();
    }
}
