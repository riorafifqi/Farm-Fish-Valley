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
        if(harvest.isApplied)       // if pickUp action is success
        {
            if (toolToCheck == harvest.currentlyChecked)       // if player is checking the right tool
            {
                harvest.isApplied = false;      // toggle quest isApplied to avoid loop
                harvest.currentlyChecked = null;    // remove checked tool when finish
                return true;
            }
        }

        return false;
    }

}
