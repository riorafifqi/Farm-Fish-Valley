using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySetter : MonoBehaviour
{
    public ItemContainer inventory;
    public List<Item> itemsToAdd;
    public List<int> itemsToAddCount;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        for (int i = 0; i < 9; i++)
        {
            inventory.itemSlots[i].Clear();
        }

        for (int i = 0; i < itemsToAdd.Count; i++)
        {
            inventory.Add(itemsToAdd[i], itemsToAddCount[i]);
        }
    }
}
