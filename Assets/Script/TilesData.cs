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
    public string TileName, description;

}
