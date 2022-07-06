using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public Vector3Int cropPosition;
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap environmentTilemap;
    [SerializeField] Tilemap cropsTilemap;


    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop == null) { continue; }

            cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                if (cropTile.growStage < cropTile.crop.sprites.Count)
                    cropTile.growStage += 1;
                cropsTilemap.SetTile(cropTile.cropPosition, cropTile.crop.sprites[cropTile.growStage]);
            }

            if(cropTile.growTimer >= cropTile.crop.timeToGrow)
            {
                Debug.Log("Done growing");
            }
        }
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        CreatePlowedTile(position);
        Debug.Log("Plow called");
    }

    void CreatePlowedTile(Vector3Int position)
    {
        environmentTilemap.SetTile((Vector3Int)position, plowed);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        cropsTilemap.SetTile(position, seeded);

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);
        crops[(Vector2Int)position].crop = toSeed;

        crop.cropPosition = position;
    }

    public bool isPlowed(Vector3Int position)
    {
        return environmentTilemap.GetTile(position) == plowed;
    }
}
