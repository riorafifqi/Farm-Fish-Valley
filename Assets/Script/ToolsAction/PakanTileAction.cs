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

        /*if (!mapManager.cropsManager.crops.ContainsKey((Vector2Int)gridPosition))
            return false;*/

        if (unpakanableTiles.Contains(tileToCheck))
        {
            Debug.Log("PakanableTiles does'nt contain tihs");
            return false;
        }

        mapManager.cropsManager.Pakan(gridPosition);
        isApplied = true;

        return true;
    }
}
