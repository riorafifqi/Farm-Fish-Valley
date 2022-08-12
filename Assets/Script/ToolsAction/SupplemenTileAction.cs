using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Supplemen Tile")]
public class SupplemenTileAction : ToolAction
{

    public List<TileBase> supplemenableTiles;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)        // when applyin to tilemap
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // get tilebase based on gridPosition
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);      // get tilemap based on tileBase

        TileBase tileToCheck = GameManager.instance.environmentTilemap.GetTile(gridPosition);    // to check if tiles is pakanable

        if (!tileToPlowData.isPakanable)        // check if tile is supplemenable/pakanable based on tileData
        {
            return false;
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))     // check if there is tool placed in tile
        {
            Debug.Log("Tools can't be placed");
            return false;
        }

        if (!supplemenableTiles.Contains(tileToCheck))      // check if tile is supplemenable based on tileBase
        {
            Debug.Log("PakanableTiles does'nt contain tihs");
            return false;
        }

        mapManager.cropsManager.Supplemen(gridPosition);
        isApplied = true;       // toggle supplement quest/task

        return true;
    }
}
