using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Filter Tile")]
public class FilterTileAction : ToolAction
{

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // tilemap target

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);  // tilemap target data

        if (!tileToPlowData.isFilterable)   // return if tile data isn't filterable
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))     // return if there is another tool placed
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Filter(gridPosition);
        isApplied = true;       // trigger task increment

        return true;
    }
}
