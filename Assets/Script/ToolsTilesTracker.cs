using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsTilesTracker : MonoBehaviour
{
    /*static public Dictionary<Vector2Int, TileBase> kincirs;
    static public Dictionary<Vector2Int, TileBase> pompas;
    static public Dictionary<Vector2Int, TileBase> filters;
    static public Dictionary<Vector2Int, TileBase> plows;*/

    static public List<Vector3Int> kincirs;
    static public List<Vector3Int> pompas;
    static public List<Vector3Int> filters;
    static public List<Vector3Int> plows;
    static public List<Vector3Int> pakans;

    private void Start()
    {
        /*kincirs = new Dictionary<Vector2Int, TileBase>();
        pompas = new Dictionary<Vector2Int, TileBase>();
        filters = new Dictionary<Vector2Int, TileBase>();
        plows = new Dictionary<Vector2Int, TileBase>();*/

        plows = new List<Vector3Int>();
        pompas = new List<Vector3Int>();
        filters = new List<Vector3Int>();
        kincirs = new List<Vector3Int>();
        pakans = new List<Vector3Int>();
    }

}
