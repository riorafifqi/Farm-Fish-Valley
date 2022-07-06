using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Crop : ScriptableObject
{
    public int timeToGrow;
    public Item yield;
    public int count = 1;

    public List<TileBase> sprites;
    public List<int> growthStageTime;
}
