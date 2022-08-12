using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Semai Tile")]
public class SemaiTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // get tile base
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);  // get tile data

        if (!tileToPlowData.isPlowable)     // if tile isn't plowable, action unsuccess
            return false;

        bool isComplete = mapManager.cropsManager.Seed(gridPosition, item.crop);    // semai action use same action as seed

        if (isComplete)
            isApplied = true;   // toggle semai quest

        return isComplete;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);     // remove item after used
    }
}
