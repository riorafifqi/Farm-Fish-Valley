using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Pompa Tile")]
public class PompaTileAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // tilebase target

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);  // tilebase target data

        if (!tileToPlowData.isPumpable)     // return if not pumpable
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))     // return if another tool placed
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Pompa(gridPosition);        // pompa action behavior
        isApplied = true;       // trigger pompa task

        return true;
    }
}
