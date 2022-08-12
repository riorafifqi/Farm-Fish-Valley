using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject          // parent of all tools action
{
    public bool isApplied = false;

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)     // when item is going to use
    {
        Debug.LogWarning("OnApplyToTilemap is not inmplemented");
        return true;
    }

    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)      // after item is used
    {
        Debug.LogWarning("OnItemUsed is not inmplemented");
    }
}
