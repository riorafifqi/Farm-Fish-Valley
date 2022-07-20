using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Quest/Check Quest")]
public class CheckQuest : QuestCompleteCondition
{
    public TileBase toolToCheck;
    public override bool Condition()
    {
        if(harvest.isApplied)
        {
            if (toolToCheck == harvest.currentlyChecked)
            {
                harvest.isApplied = false;
                harvest.currentlyChecked = null;
                return true;
            }
        }

        return false;
    }

}
