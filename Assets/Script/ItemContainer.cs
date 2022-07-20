using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> itemSlots;

    public void Add(Item item, int count = 1)
    {
        if (item.isStackable == true)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = itemSlots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
            
    }

    public void Remove(Item itemToRemove, int count = 1)
    {
        if (itemToRemove.isStackable)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.item == itemToRemove);
            if (itemSlot == null) { return; }

            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = itemSlots.Find(x => x.item == itemToRemove);
                if (itemSlot == null) { return; }

                itemSlot.Clear();
            }
        }
    }
}
