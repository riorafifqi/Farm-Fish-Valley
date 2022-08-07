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

            if(cropTile.isFed && cropTile.isHealthy && !cropTile.isAbnormal)
                cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                if (cropTile.growTimer < cropTile.crop.timeToGrow)
                    cropTile.growStage += 1;
                
                if (!cropTile.isHealthy)
                {
                    cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.unhealthyCrop);
                }
                else
                {
                    cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.sprites[cropTile.growStage]);
                }
            }

            if (!cropTile.isHealthy)
            {
                cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.unhealthyCrop);
            }

            if (cropTile.growTimer >= cropTile.crop.timeToGrow)
            {
                //Debug.Log("Done growing");
                cropTile.growFully = true;
            }
        }
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        if(!ToolsTilesTracker.plows.Contains(position))
            ToolsTilesTracker.plows.Add(position);

        CreatePlowedTile(position, plowed);
        Debug.Log(ToolsTilesTracker.plows.Count);
    }

    public void Kincir(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        if(!ToolsTilesTracker.kincirs.Contains(position))
            ToolsTilesTracker.kincirs.Add(position);

        CreatePlowedTile(position, kincired);
        Debug.Log(ToolsTilesTracker.kincirs.Count);
    }

    public void Pompa(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;
        
        if(!ToolsTilesTracker.pompas.Contains(position))
            ToolsTilesTracker.pompas.Add(position);

        CreatePlowedTile(position, pumped);
        Debug.Log(ToolsTilesTracker.pompas.Count);
    }

    public void Filter(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;
        if (!ToolsTilesTracker.filters.Contains(position))
            ToolsTilesTracker.filters.Add(position);

        CreatePlowedTile(position, filtered);
        Debug.Log(ToolsTilesTracker.filters.Count);
    }

    public void Pakan(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))
            return;

        if (crops[(Vector2Int)position].crop.name.Contains("Semai"))
        {
            CreatePlowedTile(position, pakanedSemai);
            pakanSemaiQuest.cropTile = crops[(Vector2Int)position];
        }
        else
        {
            CreatePlowedTile(position, pakaned);
            pakanQuest.cropTile = crops[(Vector2Int)position];

            if (!ToolsTilesTracker.pakans.Contains(position))
                ToolsTilesTracker.pakans.Add(position);
        }

        crops[(Vector2Int)position].isFed = true;

        Debug.Log("Pakan called");
    }

    public void Supplemen(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))
            return;

        //CreatePlowedTile(position, pakaned);    // supplement give same tile effect as pakan
        crops[(Vector2Int)position].isHealthy = true;
    }

    public void Pupuk(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))
            return;

        CreatePlowedTile(position, pupuked);    // pupuk give same tile effect as pakan
        crops[(Vector2Int)position].isPupuked = true;
    }

    void CreatePlowedTile(Vector3Int position, TileBase tileBase)
    {
        environmentTilemap.SetTile((Vector3Int)position, tileBase);
    }

    public bool Seed(Vector3Int position, Crop toSeed)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return false;

        if(canBeSeeded(position))
        {
            Debug.Log("Can't seed");
            return false;
        }

        cropsTilemap.SetTile(position, seeded);
        if (ToolsTilesTracker.plows.Contains(position))
            ToolsTilesTracker.plows.Remove(position);   // remove plows coordinate in ToolsTilesTracker if seed is planted

        if (environmentTilemap.GetTile(position) == plowed)
        {
            TileBase scoreTile = scoreManager.GetTileBase(position);
            TilesData scoreData = scoreManager.GetTileData(scoreTile);
            scoreManager.AddScore(scoreData.score);
        }

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);
        crops[(Vector2Int)position].crop = toSeed;
        crops[(Vector2Int)position].isPupuked = crops[(Vector2Int)position].crop.isPupuked;

        crop.cropPosition = position;

        return true;
    }

    public bool isPlowed(Vector3Int position)
    {
        return environmentTilemap.GetTile(position) == plowed || environmentTilemap.GetTile(position) == tileSemai;
    }

    public bool canBeSeeded(Vector3Int position)
    {
        return environmentTilemap.GetTile(position) == kincired && environmentTilemap.GetTile(position) == pumped && environmentTilemap.GetTile(position) == filtered;
    }

    public bool isToolPlaced(Vector3Int position)
    {
        return environmentTilemap.GetTile(position) == kincired || environmentTilemap.GetTile(position) == pumped || environmentTilemap.GetTile(position) == filtered;
    }

    internal bool PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if(!crops.ContainsKey(position)) { return false; }

        CropTile cropTile = crops[position];
        panenQuest.cropTile = cropTile;

        Debug.Log(cropTile.isAbnormal);

        if (cropTile.growFully && cropTile.isHealthy && cropTile.isPupuked && !cropTile.isAbnormal)
        {
            inventory.Add(cropTile.crop.yield, 1);
            cropsTilemap.SetTile(gridPosition, null);

            environmentTilemap.SetTile(gridPosition, null); // remove plowed tile

            ToolsTilesTracker.pakans.Remove(gridPosition);
            ToolsTilesTracker.plows.Remove(gridPosition);

            crops.Remove(position);

            return true;
        }
        else if(cropTile.isAbnormal)
        {
            Debug.Log("Crop is abnormal");
            inventory.Add(cropTile.crop.failYield, 1);
            cropsTilemap.SetTile(gridPosition, null);

            environmentTilemap.SetTile(gridPosition, null); // remove plowed tile

            ToolsTilesTracker.pakans.Remove(gridPosition);
            ToolsTilesTracker.plows.Remove(gridPosition);

            crops.Remove(position);

            return true;
        }

        return false;
    }

    public bool CheckTool(Vector3Int gridPosition)
    {
        if (isChecking)
            return false;

        currentToolTimer = 0f;
        checkSlider.value = currentToolTimer;
        checkSlider.maxValue = checkToolTimer;

        checkUI.transform.position = gridPosition + new Vector3(0.5f,1.5f,0);
        checkUI.SetActive(true);
        isChecking = true;


        return true;
        
    }
}
