using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsController : MonoBehaviour
{
    CropsManager cropsManager;
    MapManager mapManager;

    Vector3Int selectedTile;

    private void Awake()
    {
        cropsManager = gameObject.GetComponent<CropsManager>();
        mapManager = gameObject.GetComponent<MapManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectTile();
        if (Input.GetMouseButtonDown(0))
        {
            UseToolGrid();
        }
    }

    private void UseToolGrid()
    {
        TileBase selectedTileBase = mapManager.GetTileBase(selectedTile);
        TilesData selectedTileData = mapManager.GetTileData(selectedTileBase);

        if (!selectedTileData.isPlowable)
            return;

        if(cropsManager.isPlowed(selectedTile))
        {
            cropsManager.Seed(selectedTile);
        }
        else 
            cropsManager.Plow(selectedTile);
    }

    private void SelectTile()
    {
        selectedTile = mapManager.GetGridPosition(Input.mousePosition, true);
    }
}
