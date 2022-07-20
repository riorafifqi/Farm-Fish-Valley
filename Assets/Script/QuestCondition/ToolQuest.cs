using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Tool Quest")]
public class ToolQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        if (toolAction.isApplied)
        {
            toolAction.isApplied = false;
            return true;
        }

        return false;
        
    }
}
