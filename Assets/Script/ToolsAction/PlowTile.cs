using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ToolAction/Plow Tile")]
public class PlowTile : ToolAction
{
    [SerializeField]List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {

        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);     // plow tile tilebase
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);  // plow tile tile data

        if (!tileToPlowData.isPlowable)
        {
            return false;
        }

        mapManager.cropsManager.Plow(gridPosition);
        isApplied = true;       // trigger plow task progress

        return true;
    }
}
