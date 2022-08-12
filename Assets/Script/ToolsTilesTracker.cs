using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsTilesTracker : MonoBehaviour
{
    // use to track each tools in field
    static public List<Vector3Int> kincirs;
    static public List<Vector3Int> pompas;
    static public List<Vector3Int> filters;
    static public List<Vector3Int> plows;
    static public List<Vector3Int> pakans;

    private void Start()
    {
        // set list
        plows = new List<Vector3Int>();
        pompas = new List<Vector3Int>();
        filters = new List<Vector3Int>();
        kincirs = new List<Vector3Int>();
        pakans = new List<Vector3Int>();
    }

}
