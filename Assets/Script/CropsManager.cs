using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public Vector3Int cropPosition;

    public bool growFully = false;
    public bool isFed = false;
    public bool isHealthy = true;
    public bool isAbnormal = false;
    public bool isPupuked = true;
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase kincired;
    [SerializeField] TileBase pumped;
    [SerializeField] TileBase filtered;
    [SerializeField] TileBase pakaned;
    [SerializeField] TileBase pakanedSemai;
    [SerializeField] TileBase pupuked;

    [SerializeField] TileBase tileSemai;

    [SerializeField] Tilemap environmentTilemap;
    public Tilemap cropsTilemap;
    [SerializeField] Tilemap scoreTilemap;

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] ItemContainer inventory;

    // for check tool
    public float checkToolTimer = 5f;
    public float currentToolTimer = 5f;
    public Slider checkSlider;
    public GameObject checkUI;
    public bool isChecking = false;
    public TileBase currentlyCheckingTool;

    // for quest
    public QuestCompleteCondition panenQuest, pakanSemaiQuest, pakanQuest;

    public Dictionary<Vector2Int, CropTile> crops;

    private void Awake()
    {
        environmentTilemap = GameObject.Find("Grid").transform.GetChild(2).GetComponent<Tilemap>();
        cropsTilemap = GameObject.Find("Grid").transform.GetChild(3).GetComponent<Tilemap>();
        scoreTilemap = GameObject.Find("Grid").transform.GetChild(4).GetComponent<Tilemap>();
    }

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    private void Update()
    {
        // check timer update
        if(currentToolTimer < checkToolTimer)
        {
            currentToolTimer += Time.deltaTime;
            checkSlider.value = currentToolTimer;
            if(currentToolTimer >= checkToolTimer)
            {
                // Checking finish
                currentToolTimer = 5f;
                checkUI.SetActive(false);
                isChecking = false;
            }
        }
    }

    public void Tick()
    {
        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop == null) { continue; }

            if(cropTile.isFed && cropTile.isHealthy && !cropTile.isAbnormal)    // crops grow only if fed, healthy, and normal
                cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])        // if grow timer exceed growthStageTime
            {
                if (cropTile.growTimer < cropTile.crop.timeToGrow)      // if not fully grow
                    cropTile.growStage += 1;    // increase grow stage to change sprite
                
                if (!cropTile.isHealthy)
                {
                    cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.unhealthyCrop);       // change sprite to unhealthy if status isn't healthy
                }
                else
                {
                    cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.sprites[cropTile.growStage]);     // change sprite based on growStage
                }
            }

            if (!cropTile.isHealthy)
            {
                cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.unhealthyCrop);   // change sprite to unhealthy if status isn't healthy
            }

            if (cropTile.growTimer >= cropTile.crop.timeToGrow)     // if growTimer exceed timeToGrow
            {
                //Debug.Log("Done growing");
                cropTile.growFully = true;          // cropTile is growFully
            }
        }
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))        // if there is crops placed
            return;

        if(!ToolsTilesTracker.plows.Contains(position))     // add plow if no plow in that coord yet
            ToolsTilesTracker.plows.Add(position);

        CreatePlowedTile(position, plowed);         // create plowed tile in the coord
        Debug.Log(ToolsTilesTracker.plows.Count);
    }

    public void Kincir(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))        // if there is crops placed
            return;

        if(!ToolsTilesTracker.kincirs.Contains(position))   // add kincir if there's no kincir in that coord yet
            ToolsTilesTracker.kincirs.Add(position);

        CreatePlowedTile(position, kincired);           // add kincir tile in that coord
        //Debug.Log(ToolsTilesTracker.kincirs.Count);
    }

    public void Pompa(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))    // if there is crops placed
            return;
        
        if(!ToolsTilesTracker.pompas.Contains(position))    // add pompa to list if no pompa placed in that coord yet
            ToolsTilesTracker.pompas.Add(position);

        CreatePlowedTile(position, pumped);     // add pompa tile in that coord
        //Debug.Log(ToolsTilesTracker.pompas.Count);
    }

    public void Filter(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))    // if there is crops placed
            return;
        if (!ToolsTilesTracker.filters.Contains(position))  // add filter to list if no fliter placed in that coord yet
            ToolsTilesTracker.filters.Add(position);    

        CreatePlowedTile(position, filtered);   // add filter tile in that coord
        Debug.Log(ToolsTilesTracker.filters.Count);
    }

    public void Pakan(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))   // if no crops placed
            return;

        if (crops[(Vector2Int)position].crop.name.Contains("Semai"))    // if it's semai seed
        {
            CreatePlowedTile(position, pakanedSemai);   // create pakan semai tile
            pakanSemaiQuest.cropTile = crops[(Vector2Int)position];
        }
        else
        {
            CreatePlowedTile(position, pakaned);        // create normal pakan tile
            pakanQuest.cropTile = crops[(Vector2Int)position];

            if (!ToolsTilesTracker.pakans.Contains(position))   // add pakan if no pakan placed in the coord
                ToolsTilesTracker.pakans.Add(position);
        }

        crops[(Vector2Int)position].isFed = true;       // crops isFed status to true to start growing

        Debug.Log("Pakan called");
    }

    public void Supplemen(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))       // can't place if no crops
            return;

        //CreatePlowedTile(position, pakaned);    // supplement give same tile effect as pakan
        crops[(Vector2Int)position].isHealthy = true;   // change crops status in that coord to healthy
    }

    public void Pupuk(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))   // can't place if no crops
            return;

        CreatePlowedTile(position, pupuked);    // pupuk give same tile effect as pakan
        crops[(Vector2Int)position].isPupuked = true;   // change crops status in that coord to pupuked
    }

    void CreatePlowedTile(Vector3Int position, TileBase tileBase)
    {
        environmentTilemap.SetTile((Vector3Int)position, tileBase);     // create tile spirit in that coord
    }

    public bool Seed(Vector3Int position, Crop toSeed)
    {
        if (crops.ContainsKey((Vector2Int)position))        // can't place if there is already seed in that coord
            return false;

        if(canBeSeeded(position))   // can't seed if there's tool etc
        {
            Debug.Log("Can't seed");
            return false;
        }

        cropsTilemap.SetTile(position, seeded);     // set tile sprite to crops sprite
        if (ToolsTilesTracker.plows.Contains(position))
            ToolsTilesTracker.plows.Remove(position);   // remove plows coordinate in ToolsTilesTracker if seed is planted

        if (environmentTilemap.GetTile(position) == plowed)     
        {
            TileBase scoreTile = scoreManager.GetTileBase(position);
            TilesData scoreData = scoreManager.GetTileData(scoreTile);
            scoreManager.AddScore(scoreData.score);     // get score based on crops tile data placed in that area (check Grid>Tilemap_Scoring)
        }

        // add crops to list of crops
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop); 
        crops[(Vector2Int)position].crop = toSeed;
        crops[(Vector2Int)position].isPupuked = crops[(Vector2Int)position].crop.isPupuked;     // set pupuked status based on crops data

        crop.cropPosition = position;

        return true;
    }

    public bool isPlowed(Vector3Int position)   // check if tile is plowed
    {
        return environmentTilemap.GetTile(position) == plowed || environmentTilemap.GetTile(position) == tileSemai;
    }

    public bool canBeSeeded(Vector3Int position)    // check if tile can be seed
    {
        // can't seed if tool is placed
        return environmentTilemap.GetTile(position) == kincired && environmentTilemap.GetTile(position) == pumped && environmentTilemap.GetTile(position) == filtered;
    }

    public bool isToolPlaced(Vector3Int position)   // check if tool is placed in that coord
    {
        return environmentTilemap.GetTile(position) == kincired || environmentTilemap.GetTile(position) == pumped || environmentTilemap.GetTile(position) == filtered;
    }

    internal bool PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if(!crops.ContainsKey(position)) { return false; }      // return if no crops to harvest

        CropTile cropTile = crops[position];    // set cropTile to target crops
        panenQuest.cropTile = cropTile;

        Debug.Log(cropTile.isAbnormal);

        // can be harvested if crop is fully grow, healthy, pupuk, and normal
        if (cropTile.growFully && cropTile.isHealthy && cropTile.isPupuked && !cropTile.isAbnormal)  
        {
            inventory.Add(cropTile.crop.yield, 1);      // add crop yield to inventory
            cropsTilemap.SetTile(gridPosition, null);   // remove crop sprite from map

            environmentTilemap.SetTile(gridPosition, null); // remove plowed tile

            ToolsTilesTracker.pakans.Remove(gridPosition);  // remove coord from pakan list
            ToolsTilesTracker.plows.Remove(gridPosition);   // remove coord from plow list

            crops.Remove(position);     // remove crops from crops list

            return true;
        }
        else if(cropTile.isAbnormal)    // if crops is abnormal
        {
            Debug.Log("Crop is abnormal");
            inventory.Add(cropTile.crop.failYield, 1);  // harvest fail yield instead
            cropsTilemap.SetTile(gridPosition, null);   // remove crop sprite from map

            environmentTilemap.SetTile(gridPosition, null); // remove plowed tile

            ToolsTilesTracker.pakans.Remove(gridPosition);  // remove coord from pakan list
            ToolsTilesTracker.plows.Remove(gridPosition);   // remove coord from plow list

            crops.Remove(position);     // remove crops from crops list

            return true;
        }

        return false;
    }

    public bool CheckTool(Vector3Int gridPosition)
    {
        if (isChecking)     // can't check if another check is in progress
            return false;

        currentToolTimer = 0f;      // set timer to 0
        checkSlider.value = currentToolTimer;   // set slider value to 0
        checkSlider.maxValue = checkToolTimer;  // set slider max value based on time needed

        checkUI.transform.position = gridPosition + new Vector3(0.5f,1.5f,0);   // activate check slider UI above tools
        checkUI.SetActive(true);
        isChecking = true;


        return true;
        
    }
}
