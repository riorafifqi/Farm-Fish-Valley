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
        mapManager = gameObject.GetComponent<MapManager>();
        toolsController = gameObject.GetComponent<ToolsController>();
    }
    
    void Start()
    {
        //DescriptionBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (toolsController.usedItem.isStackable)
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = environmentMap.WorldToCell(mousePos);

            TileBase clickedTile;
            if ((clickedTile = cropsMap.GetTile(gridPos)) == null)
                clickedTile = environmentMap.GetTile(gridPos);
            
            if (clickedTile == null)
                return;

            if (mapManager.GetTileData(clickedTile).isDescriptive)
            {
                objectName.text = mapManager.GetTileData(clickedTile).TileName;
                objectDescription.text = mapManager.GetTileData(clickedTile).description;

                DescriptionBox.transform.position = gridPos;
                DescriptionBox.SetActive(true);
            }
        }

        /*if(Input.anyKeyDown)
        {
            DescriptionBox.SetActive(false);
        }*/
    }
}
