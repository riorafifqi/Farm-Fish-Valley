using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsController : MonoBehaviour
{
    ToolbarController toolbarController;
    MapManager mapManager;
    public Item testItem;

    Vector3Int selectedTile;

    private void Awake()
    {
        mapManager = gameObject.GetComponent<MapManager>();
        toolbarController = GetComponent<ToolbarController>();
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
        Item item = toolbarController.GetItem;
        if (item == null) { return;  }
        if (item.onTileMapAction == null) { return;  }

        //Animator.SetTrigger("act") // for action animator

        bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTile, mapManager, item);

        if (complete)
            if (item.onItemUsed != null)
                item.onItemUsed.OnItemUsed(item, GameManager.instance.itemContainer);
    }

    private void SelectTile()
    {
        selectedTile = mapManager.GetGridPosition(Input.mousePosition, true);
    }
}
