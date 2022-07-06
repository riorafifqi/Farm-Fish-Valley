using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ToolAction/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        if (!mapManager.cropsManager.isPlowed(gridPosition))
            return false;

        mapManager.cropsManager.Seed(gridPosition, item.crop);

        return true;
    }
}
