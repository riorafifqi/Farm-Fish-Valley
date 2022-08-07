using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragNDropController.OnClick(inventory.itemSlots[id]);
        Show();
    }

    private void Update()
    {
        Show();
    }


}
