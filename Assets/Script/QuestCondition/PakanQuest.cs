using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Pakan Quest")]
public class PakanQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        if(toolAction.isApplied)    // if pakan action is successfully applied
        {
            if (!cropTile.crop.name.Contains("Semai"))      // if the one that pakan-ed is semai crops, dont increment quest
            {
                cropTile = null;        
                toolAction.isApplied = false;       // toggle back isApplied to false to prevent loop
                return true;
            }
        }

        return false;
    }

}
