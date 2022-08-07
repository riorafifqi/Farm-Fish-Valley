using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Crop : ScriptableObject
{
    public int timeToGrow;
    public Item yield;
    public Item failYield;
    public int count = 1;
    public bool isPupuked = true;

    public TileBase unhealthyCrop;
    public List<TileBase> sprites;
    public List<int> growthStageTime;
}
