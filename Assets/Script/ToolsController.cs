using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsController : MonoBehaviour
{
    MapManager mapManager;
    public Item testItem;

    Vector3Int selectedTile;

    private void Awake()
    {
        mapManager = gameObject.GetComponent<MapManager>();
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
        Item item = testItem;
        bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTile, mapManager, item);
    }

    private void SelectTile()
    {
        selectedTile = mapManager.GetGridPosition(Input.mousePosition, true);
    }
}
