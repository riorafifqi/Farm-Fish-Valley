using System;
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

    [SerializeField] TileBase tileSemai;

    [SerializeField] Tilemap environmentTilemap;
    [SerializeField] Tilemap cropsTilemap;
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

            if(cropTile.isFed)
                cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                if (cropTile.growTimer < cropTile.crop.timeToGrow)
                    cropTile.growStage += 1;
                cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.sprites[cropTile.growStage]);
            }

            if(cropTile.growTimer >= cropTile.crop.timeToGrow)
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

        CreatePlowedTile(position, plowed);
        Debug.Log("Plow called");
    }

    public void Kincir(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        CreatePlowedTile(position, kincired);
        Debug.Log("Kincir called");
    }

    public void Pompa(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        CreatePlowedTile(position, pumped);
        Debug.Log("Pompa called");
    }

    public void Filter(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        CreatePlowedTile(position, filtered);
        Debug.Log("Filter called");
    }

    public void Pakan(Vector3Int position)
    {
        if (!crops.ContainsKey((Vector2Int)position))
            return;

        if (crops[(Vector2Int)position].crop.name.Contains("Semai"))
        {
            pakanSemaiQuest.cropTile = crops[(Vector2Int)position];
            CreatePlowedTile(position, pakanedSemai);
        }
        else
        {
            pakanQuest.cropTile = crops[(Vector2Int)position];
            CreatePlowedTile(position, pakaned);
        }

        crops[(Vector2Int)position].isFed = true;

        Debug.Log("Pakan called");
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
        if (environmentTilemap.GetTile(position) == plowed)
        {
            TileBase scoreTile = scoreManager.GetTileBase(position);
            TilesData scoreData = scoreManager.GetTileData(scoreTile);
            scoreManager.AddScore(scoreData.score);
        }

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);
        crops[(Vector2Int)position].crop = toSeed;

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

        if (cropTile.growFully)
        {
            inventory.Add(cropTile.crop.yield, 1);
            cropsTilemap.SetTile(gridPosition, null);
            environmentTilemap.SetTile(gridPosition, null);

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
