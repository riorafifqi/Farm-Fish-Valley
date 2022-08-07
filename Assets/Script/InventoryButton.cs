using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image highlight;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;
        icon.gameObject.SetActive(true);
        text.gameObject.SetActive(true);

        if (slot.item.isStackable)
        {
            text.text = slot.count.ToString();
        }
        else
            text.gameObject.SetActive(false);
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*ItemContainer inventory = GameManager.instance.itemContainer;
        GameManager.instance.dragNDropController.OnClick(inventory.itemSlots[myIndex]);
        transform.parent.GetComponent<InventoryPanel>().Show();*/

        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);
    }

    public void Highlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}