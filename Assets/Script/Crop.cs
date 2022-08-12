using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Crop : ScriptableObject
{
    public int timeToGrow;  // time needed to fully grow
    public Item yield;  // if success
    public Item failYield;  // if crop is fail
    public int count = 1;
    public bool isPupuked = true;       // by default, crops ready to harvest

    public TileBase unhealthyCrop;      // to change sprite when it's unhealthy
    public List<TileBase> sprites;      // sprite stage
    public List<int> growthStageTime;   // time needed for each stage, when time reached, crops change sprite based on its stage
}
