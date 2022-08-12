using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragNDropController.OnClick(inventory.itemSlots[id]);      // drag item
        Show(); // update panel onclick
    }

    private void Update()
    {
        Show();     // update panel 
    }


}
