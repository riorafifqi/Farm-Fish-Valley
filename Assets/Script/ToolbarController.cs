using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 5;
    int selectedTool;
    [SerializeField] Text toolUseText;

    public Action<int> onChange;

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;     // mouse scroll controller
        if (delta != 0)
        {
            if (delta > 0)  // scroll up
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else   // scroll down
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }

        if (GetItem == null)        // no item highlighted
            toolUseText.text = " ";
        else
            toolUseText.text = GetItem.name;  // show currently used item name
    }

    internal void Set(int id)
    {
        selectedTool = id;  // set id to selected tool
    }

    public Item GetItem
    {
        get
        {
            return GameManager.instance.itemContainer.itemSlots[selectedTool].item;     // get selected tool item in inventory
        }
    }
}
