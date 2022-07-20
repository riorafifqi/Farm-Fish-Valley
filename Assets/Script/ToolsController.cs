using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System;

public class ToolsController : MonoBehaviour
{
    ToolbarController toolbarController;
    MapManager mapManager;
    ToolPlacementManager toolPlacementManager;
    [SerializeField] ScoreManager scoreManager;
    public Item usedItem;
    [SerializeField] ToolAction onTilePickUp;

    Vector3Int selectedTile;

    private void Awake()
    {
        toolPlacementManager = gameObject.GetComponent<ToolPlacementManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        toolbarController = GetComponent<ToolbarController>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectTile();

        if (Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
                UseToolGrid();
        }
    }

    private void PickUpTile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTile, mapManager, null);
    }

    private void UseToolGrid()
    {
        //Animator.SetTrigger("act") // for action animator
        usedItem = toolbarController.GetItem;
        if (usedItem == null)
        {
            PickUpTile();
            return;
        }
        if (usedItem.onTileMapAction == null) { return; }

        bool complete = false;

        /*if (mapManager.GetTileData(mapManager.GetTileBase(selectedTile)).isDescriptive)
            if (!usedItem.isStackable)
                return;*/

        if (usedItem.isStackable)
        {
            if (usedItem.name.Contains("Semai"))
                complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, toolPlacementManager, usedItem);
            else
                complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, mapManager, usedItem);   // if not tools/consumable
        }
        else
            complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, toolPlacementManager, usedItem);   // if tools

        if (complete)
        {
            Debug.Log("You get " + usedItem.successScore + " point");
            scoreManager.AddScore(usedItem.successScore);
            if (usedItem.onItemUsed != null)
                usedItem.onItemUsed.OnItemUsed(usedItem, GameManager.instance.itemContainer);
        }
        else
        {
            Debug.Log("You lose " + usedItem.failScore + " point");
            scoreManager.AddScore(usedItem.failScore);
        }
            
    }

    private void SelectTile()
    {
        selectedTile = mapManager.GetGridPosition(Input.mousePosition, true);
    }
}
