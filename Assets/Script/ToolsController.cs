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

    void Update()
    {
        SelectTile();

        if (Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())      // if pointer isn't over UI
                UseToolGrid();
        }
    }

    private void PickUpTile()       // for harvest or check tool
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTile, mapManager, null);
    }

    private void UseToolGrid()      // use currently equipped item
    {
        //Animator.SetTrigger("act") // if there is an action animator

        usedItem = toolbarController.GetItem;       // get currently used item
        if (usedItem == null)       // if no item used, call pickupTile
        {
            PickUpTile();
            return;
        }
        if (usedItem.onTileMapAction == null) { return; }

        bool complete = false;

        if (usedItem.isStackable)
        {
            if (usedItem.name.Contains("Semai"))        // if used item is un-semai-ed seed
                complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, toolPlacementManager, usedItem);
            else
                complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, mapManager, usedItem);   // if not tools/consumable item
        }
        else
            complete = usedItem.onTileMapAction.OnApplyToTileMap(selectedTile, toolPlacementManager, usedItem);   // if tools

        if (complete)       // if item is successfully applied to tileMap
        {
            Debug.Log("You get " + usedItem.successScore + " point");
            scoreManager.AddScore(usedItem.successScore);       // add score based on usedItem
            if (usedItem.onItemUsed != null)
                usedItem.onItemUsed.OnItemUsed(usedItem, GameManager.instance.itemContainer);       // to reduce stackable item count in inventory
        }
        else
        {
            Debug.Log("You lose " + usedItem.failScore + " point");     // if item isn't successfully applied
            scoreManager.AddScore(usedItem.failScore);      // reduce score
        }
            
    }

    private void SelectTile()
    {
        selectedTile = mapManager.GetGridPosition(Input.mousePosition, true);       // assign selected tile to mouse current position
    }
}
