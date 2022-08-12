using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)     // copy item to slot destination
    {
        item = slot.item;
        count = slot.count;
    }

    public void Set(Item item, int count)       // set item and count
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()     // clear item in slot
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> itemSlots;        // amount of  inventory

    public void Add(Item item, int count = 1)
    {
        if (item.isStackable == true)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == item);        // find item slot with same type
            if (itemSlot != null)
            {
                itemSlot.count += count;    // increase count if item is available
            }
            else
            {
                itemSlot = itemSlots.Find(x => x.item == null);     // find empty slot
                if (itemSlot != null)
                {
                    itemSlot.item = item;       // set empty slot to target item
                    itemSlot.count = count;
                }
            }
        }
        else  // item isn't stackable
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == null);    // find empty slot
            if (itemSlot != null)
            {
                itemSlot.item = item;   // set empty slot to target item
            }
        }
            
    }

    public void Remove(Item itemToRemove, int count = 1)
    {
        if (itemToRemove.isStackable)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == itemToRemove);        // find target item in slot
            if (itemSlot == null) { return; }

            itemSlot.count -= count;    // reduce count
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();   // remove item from slot if item amount is 0
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = itemSlots.Find(x => x.item == itemToRemove);    // find target item in slot
                if (itemSlot == null) { return; }

                itemSlot.Clear();       // clear item slot
            }
        }
    }
}
