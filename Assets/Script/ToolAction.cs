using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not inmplemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        Debug.LogWarning("OnApplyToTilemap is not inmplemented");
        return true;
    }

    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        Debug.LogWarning("OnItemUsed is not inmplemented");
    }
}
