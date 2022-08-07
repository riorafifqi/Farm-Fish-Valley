using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleteCondition : ScriptableObject
{
    public ToolAction toolAction;

    public TilePickUp harvest;
    public CropTile cropTile;

    public virtual bool Condition()
    {
        Debug.Log("Condition on base get Called");
        return true;
    }
}
