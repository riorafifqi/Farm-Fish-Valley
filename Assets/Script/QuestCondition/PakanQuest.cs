using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Pakan Quest")]
public class PakanQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        Debug.Log(cropTile.crop.name);
        if(toolAction.isApplied)
        {
            if (!cropTile.crop.name.Contains("Semai"))
            {
                cropTile = null;
                toolAction.isApplied = false;
                return true;
            }
        }

        return false;
    }

}
