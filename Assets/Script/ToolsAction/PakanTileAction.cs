using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Pakan Tile")]
public class PakanTileAction : ToolAction
{

    public List<TileBase> unpakanableTiles;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        TileBase tileToCheck = GameManager.instance.environmentTilemap.GetTile(gridPosition);    // to check if tiles is pakanable

        if (!tileToPlowData.isPakanable)    // if tile data isn't pakanable
        {
            Debug.Log("No pakanabel");
            return false;       // action failed
        }

        if (mapManager.cropsManager.isToolPlaced(gridPosition))     // if there is a tool placed in the grid
        {
            Debug.Log("Tools can't be placed");
            return false;       // action failed
        }

        if (unpakanableTiles.Contains(tileToCheck))     // if tiles is not pakanable
        {
            return false;       // action failed
        }

        mapManager.cropsManager.Pakan(gridPosition);
        isApplied = true;       // to trigger task/quest

        return true;        // action success
    }
}
