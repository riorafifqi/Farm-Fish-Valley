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
            buttons[i].SetIndex(i);     // set index to each itemslot
        }
    }

    public void Show()
    {
        // update itemslot UI
        for (int i = 0; i < inventory.itemSlots.Count && i < buttons.Count; i++)
        {
            if (inventory.itemSlots[i].item == null && inventory.itemSlots[i].count == 0)   //  if no item and count is 0
            {
                buttons[i].Clean();     // clean button slot
            }
            else  // if there is an item
            {
                buttons[i].Set(inventory.itemSlots[i]);     // set button item slot to correspondence item
            }
        }
    }

    public virtual void OnClick(int id)
    {
        
    }
}
