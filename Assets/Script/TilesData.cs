using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TilesData : ScriptableObject
{
    public TileBase[] tiles;

    public bool isPlowable;
    public bool isDescriptive;

    public bool isKincirable;
    public bool isPumpable;
    public bool isFilterable;
    public bool isPakanable;

    public bool isTool;

    public string TileName, description;
    public int score;
}
