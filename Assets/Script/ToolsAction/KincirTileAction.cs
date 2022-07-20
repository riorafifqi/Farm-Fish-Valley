using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Kincir Tile")]
public class KincirTileAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        if (!tileToPlowData.isKincirable)
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        mapManager.cropsManager.Kincir(gridPosition);
        isApplied = true;

        return true;
    }
}
