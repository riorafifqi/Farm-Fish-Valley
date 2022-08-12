using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string name;
    public bool isStackable;
    public Sprite icon;     // sprite icon in UI
    public ToolAction onTileMapAction;  // item behavior when used
    public ToolAction onItemUsed;   // item behavior after used
    public Crop crop;       // related crop for seed item

    public int successScore;    // score if success
    public int failScore;       // score if fail
}
