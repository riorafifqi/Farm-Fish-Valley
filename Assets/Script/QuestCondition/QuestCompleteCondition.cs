using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleteCondition : ScriptableObject
{
    // Parent of normal task complete condition (only normal quest)
    public ToolAction toolAction;   // related tool action (ex: task beri pakan = PakanTileAction)

    public TilePickUp harvest;      // for harvest related task
    public CropTile cropTile;      

    public virtual bool Condition()
    {
        Debug.Log("Condition on base get Called");
        return true;
    }
}
