using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Tool Quest")]
public class ToolQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        if (toolAction.isApplied)       // if tools is successfully applied
        {
            toolAction.isApplied = false;   // toggle back isApplied to false to prevent loop
            return true;
        }

        return false;
        
    }
}
