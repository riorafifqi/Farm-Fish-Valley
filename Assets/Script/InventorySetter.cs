using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySetter : MonoBehaviour
{
    public ItemContainer inventory;
    public List<Item> itemsToAdd;   // item you want to add
    public List<int> itemsToAddCount;   // amount of that item, in order

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventory.itemSlots.Count; i++)     // clear all item slot
        {
            inventory.itemSlots[i].Clear();
        }

        for (int i = 0; i < inventory.itemSlots.Count; i++)     // set slot according to itemsToAdd & itemsToAddCount list
        {
            inventory.Add(itemsToAdd[i], itemsToAddCount[i]);
        }
    }
}
