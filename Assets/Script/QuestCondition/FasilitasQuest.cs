using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Fasilitas Quest")]
public class FasilitasQuest : QuestCompleteCondition
{
    public ToolAction[] toolActions;

    public override bool Condition()
    {
        foreach (ToolAction actions in toolActions)
        {
            if(actions.isApplied)
            {
                actions.isApplied = false;
                return true;
            }
        }

        return false;
    }
}
