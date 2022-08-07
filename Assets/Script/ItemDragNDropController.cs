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
            iconTransform.position = Input.mousePosition;
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if(this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        } else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if(itemSlot.item == null)
        {
            dragItemIcon.SetActive(false);
        }
        else
        {
            dragItemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
        }
    }
}
