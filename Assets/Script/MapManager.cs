using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    public CropsManager cropsManager;

    [SerializeField] public List<TilesData> tileDatas;
    private Dictionary<TileBase, TilesData> dataFromTiles;

    private void Awake()
    {
        cropsManager = gameObject.GetComponent<CropsManager>();

        dataFromTiles = new Dictionary<TileBase, TilesData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = map.WorldToCell(mousePos);

            TileBase clickedTile = map.GetTile(gridPos);

            if (dataFromTiles[clickedTile].isDescriptive)
            {
                string tileName = dataFromTiles[clickedTile].TileName;
                string description = dataFromTiles[clickedTile].description;

                Debug.Log(tileName + "'s Description : " + description);    
            }
                
        }*/
    }

    public TileBase GetTileBase(Vector3Int gridPos)
    {
        TileBase tile = map.GetTile(gridPos);

        return tile;
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;

        if(mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TilesData GetTileData(TileBase tileBase)
    {
        return dataFromTiles[tileBase];
    }
}
