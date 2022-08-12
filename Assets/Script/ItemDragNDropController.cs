using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragNDropController : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject dragItemIcon;
    RectTransform iconTransform;
    Image itemIconImage;
    

    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = dragItemIcon.GetComponent<RectTransform>();
        itemIconImage = dragItemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if(dragItemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;       // item icon position follow the cursor position when being drag
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if(this.itemSlot.item == null)  // if click on item on inventory
        {
            this.itemSlot.Copy(itemSlot);   // copy item to temp storage when being drag
            itemSlot.Clear();   // clear slot on item
        } else
        {
            Item item = itemSlot.item;      // set empty to current drag item
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);   
            this.itemSlot.Set(item, count); // set slot to dragged ite
        }
        UpdateIcon();       // update item cursor UI
    }

    private void UpdateIcon()
    {
        // if no item, cursor back to normal
        if(itemSlot.item == null)
        {
            dragItemIcon.SetActive(false);
        }
        else  // else, setactive icon to true and change sprite to currently dragged item
        {
            dragItemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
        }
    }
}
