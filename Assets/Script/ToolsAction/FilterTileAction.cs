using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Filter Tile")]
public class FilterTileAction : ToolAction
{

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        if (!tileToPlowData.isFilterable)
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Filter(gridPosition);
        isApplied = true;

        return true;
    }
}
