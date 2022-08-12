using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ToolAction/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        if (!mapManager.cropsManager.isPlowed(gridPosition))    // if tiles isn't plowed yet, crops can't be placed
            return false;

        bool isComplete = mapManager.cropsManager.Seed(gridPosition, item.crop);

        if (isComplete)     // if seed successfully placed
            isApplied = true;   // toggle isApplied to trigger seed quest/task

        return isComplete;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);     // reduce count when applied
    }
}
