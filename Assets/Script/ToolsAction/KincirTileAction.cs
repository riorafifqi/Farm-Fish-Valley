using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Kincir Tile")]
public class KincirTileAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // tilebase target

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);  // tilebase target data

        if (!tileToPlowData.isKincirable)   // return if not kincirable
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))     // return if another tool placed
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Kincir(gridPosition);
        isApplied = true;   // trigger task increment

        return true;
    }
}
