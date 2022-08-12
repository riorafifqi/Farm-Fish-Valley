using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap baseMap;         // Environment Tilemap
    public CropsManager cropsManager;

    [SerializeField] public List<TilesData> tileDatas;      // to store tiles data
    private Dictionary<TileBase, TilesData> dataFromTiles;      // to store tiles data and tile base

    protected virtual void Awake()
    {
        cropsManager = gameObject.GetComponent<CropsManager>();     // assign cropsManager

        dataFromTiles = new Dictionary<TileBase, TilesData>();      
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);          // assign tileData to specific tileBase
            }
        }
    }

    public TileBase GetTileBase(Vector3Int gridPos)     // get tileBase based on grid position
    {
        TileBase tile = baseMap.GetTile(gridPos);       

        return tile;
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)     // get grid position based on world position
    {
        Vector3 worldPosition;

        if(mousePosition)       // if the position is using the mouse cursor position
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        Vector3Int gridPosition = baseMap.WorldToCell(worldPosition);       // convert world coord to cell

        return gridPosition;
    }

    public TilesData GetTileData(TileBase tileBase)     // to get tileData based on tileBase
    {
        return dataFromTiles[tileBase];
    }
}
