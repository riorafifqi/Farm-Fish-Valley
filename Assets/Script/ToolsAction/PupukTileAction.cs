using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Pupuk Tile")]
public class PupukTileAction : ToolAction
{

    public List<TileBase> pupukableTiles;

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

        if (!pupukableTiles.Contains(tileToCheck))
        {
            Debug.Log("PakanableTiles does'nt contain tihs");
            return false;
        }

        mapManager.cropsManager.Pupuk(gridPosition);
        isApplied = true;

        return true;
    }
}
