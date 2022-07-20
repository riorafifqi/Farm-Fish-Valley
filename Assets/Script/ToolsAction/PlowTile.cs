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
        Debug.Log("OnApplyToTileMap PlowTile Called");

        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);

        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        if (!tileToPlowData.isPlowable)
        {
            return false;
        }

        mapManager.cropsManager.Plow(gridPosition);
        isApplied = true;

        return true;
    }
}
