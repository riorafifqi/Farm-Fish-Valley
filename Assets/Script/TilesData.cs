using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TilesData : ScriptableObject
{
    public TileBase[] tiles;

    public bool isPlowable;     // tile can be plowed
    public bool isDescriptive;  // tile can has description

    public bool isKincirable;   // kincir can be placed
    public bool isPumpable;     // pompa can be placed
    public bool isFilterable;   // filter can be placed
    public bool isPakanable;    // pakan can be plance

    public bool isTool;

    public string TileName, description;
    public int score;
}
