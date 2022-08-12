using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;        // item icon
    [SerializeField] Text text;         // item count text
    [SerializeField] Image highlight;   // selected item highlight

    int myIndex;        // inventory slot index (left = 0)

    public void SetIndex(int index)     // set inventory slot index
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        // set button placed item
        icon.sprite = slot.item.icon;
        icon.gameObject.SetActive(true);
        text.gameObject.SetActive(true);

        if (slot.item.isStackable)
        {
            text.text = slot.count.ToString();  // show count text UI if stackable
        }
        else
            text.gameObject.SetActive(false);
    }

    public void Clean()
    {
        // clean button to empty
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(myIndex);     // highlight and set item as used item
    }

    public void Highlight(bool b)
    {
        // highlight target item
        highlight.gameObject.SetActive(b);
    }
}