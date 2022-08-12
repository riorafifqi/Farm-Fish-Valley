using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionManager : MonoBehaviour
{
    private MapManager mapManager;
    private ToolsController toolsController;
    [SerializeField] private Tilemap environmentMap;
    [SerializeField] private Tilemap cropsMap;

    [SerializeField] private Text objectName;
    [SerializeField] private Text objectDescription;
    [SerializeField] private GameObject DescriptionBox;


    void Awake()
    {
        environmentMap = GameObject.Find("Grid").transform.GetChild(2).GetComponent<Tilemap>();
        cropsMap = GameObject.Find("Grid").transform.GetChild(3).GetComponent<Tilemap>();

        mapManager = gameObject.GetComponent<MapManager>();
        toolsController = gameObject.GetComponent<ToolsController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))        // if left mouse button pressed
        {
            if (EventSystem.current.IsPointerOverGameObject())      // if cursor above UI
                return;

            if (toolsController.usedItem.isStackable)
                return;

            // convert cursor position to cell
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = environmentMap.WorldToCell(mousePos);

            TileBase clickedTile;
            if ((clickedTile = cropsMap.GetTile(gridPos)) == null)
                clickedTile = environmentMap.GetTile(gridPos);      // if no crops clicked
            
            if (clickedTile == null)
                return;

            if (mapManager.GetTileData(clickedTile).isDescriptive)      // if tile data has description
            {
                objectName.text = mapManager.GetTileData(clickedTile).TileName;     // assign tileName to UI
                objectDescription.text = mapManager.GetTileData(clickedTile).description;   // assign tile desc to UI

                DescriptionBox.transform.position = gridPos + new Vector3(0.5f, 2, 0);  // set panel box above object
                DescriptionBox.SetActive(true); // activate gameobject
            }
        }

        /*if(Input.anyKeyDown)
        {
            DescriptionBox.SetActive(false);
        }*/
    }
}
