using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < inventory.itemSlots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < inventory.itemSlots.Count && i < buttons.Count; i++)
        {
            if (inventory.itemSlots[i].item == null && inventory.itemSlots[i].count == 0)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.itemSlots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
