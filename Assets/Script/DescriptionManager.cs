using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    private MapManager mapManager;
    [SerializeField] private Tilemap environmentMap;
    [SerializeField] private Text objectName;
    [SerializeField] private Text objectDescription;
    [SerializeField] private GameObject DescriptionBox;


    void Awake()
    {
        mapManager = gameObject.GetComponent<MapManager>();
    }
    
    void Start()
    {
        //DescriptionBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = environmentMap.WorldToCell(mousePos);

            TileBase clickedTile = environmentMap.GetTile(gridPos);

            if (mapManager.GetTileData(clickedTile).isDescriptive)
            {
                objectName.text = mapManager.GetTileData(clickedTile).TileName;
                objectDescription.text = mapManager.GetTileData(clickedTile).description;

                DescriptionBox.transform.position = gridPos;
                DescriptionBox.SetActive(true);
            }
        }
    }
}
