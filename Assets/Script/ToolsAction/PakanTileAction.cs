using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Pakan Tile")]
public class PakanTileAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        if (!tileToPlowData.isPakanable)
        {
            Debug.Log("No pakanabel");
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Pakan(gridPosition);
        isApplied = true;

        return true;
    }
}
