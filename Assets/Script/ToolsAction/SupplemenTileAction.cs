using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Supplemen Tile")]
public class SupplemenTileAction : ToolAction
{

    public List<TileBase> supplemenableTiles;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        TileBase tileToCheck = GameManager.instance.environmentTilemap.GetTile(gridPosition);    // to check if tiles is pakanable

        if (!tileToPlowData.isPakanable)
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        if (!supplemenableTiles.Contains(tileToCheck))
        {
            Debug.Log("PakanableTiles does'nt contain tihs");
            return false;
        }

        mapManager.cropsManager.Supplemen(gridPosition);
        isApplied = true;

        return true;
    }
}
