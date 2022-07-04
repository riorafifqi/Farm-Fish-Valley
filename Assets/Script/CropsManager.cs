using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{

}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Dictionary<Vector2Int, Crops> crops;

    private void Awake()
    {
        crops = new Dictionary<Vector2Int, Crops>();
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
        targetTilemap.SetTile((Vector3Int)position, plowed);
    }

    public void Seed(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
            return;

        targetTilemap.SetTile(position, seeded);
        Debug.Log("Seed called");

        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);
    }

    public bool isPlowed(Vector3Int position)
    {
        return targetTilemap.GetTile(position) == plowed;
    }
}
