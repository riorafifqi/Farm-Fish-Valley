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
        TilesData tileToPlowData = null;

        if(mapManager.GetTileData(tileToPlow))      // check if tools is placed
            tileToPlowData = mapManager.GetTileData(tileToPlow);        // get tools tile Data

        if (mapManager.cropsManager.crops.ContainsKey((Vector2Int)gridPosition))        // check if crops is placed
            isSuccess = mapManager.cropsManager.PickUp(gridPosition);       // harvest crops
        else if (tileToPlowData.isTool)
        {
            isSuccess = mapManager.cropsManager.CheckTool(gridPosition);        // check tool
            currentlyChecked = mapManager.GetTileBase(gridPosition);
        }

        if (isSuccess)
            isApplied = true;       // toggle task

        return isSuccess;
    }
}
