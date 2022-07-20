using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="ToolAction/Harvest")]
public class TilePickUp : ToolAction
{
    public TileBase currentlyChecked;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, MapManager mapManager, Item item)
    {
        bool isSuccess = false;
        TileBase tileToPlow = mapManager.GetTileBase(gridPosition);
        TilesData tileToPlowData = mapManager.GetTileData(tileToPlow);

        if (mapManager.cropsManager.crops.ContainsKey((Vector2Int)gridPosition))
            isSuccess = mapManager.cropsManager.PickUp(gridPosition);
        else if (tileToPlowData.isTool)
        {
            isSuccess = mapManager.cropsManager.CheckTool(gridPosition);
            currentlyChecked = mapManager.GetTileBase(gridPosition);
        }
            
        if (isSuccess)
            isApplied = true;

        return isSuccess;
    }
}
