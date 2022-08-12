using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Fasilitas Quest")]
public class FasilitasQuest : QuestCompleteCondition
{
    public ToolAction[] toolActions;        // list of all tool action

    public override bool Condition()
    {
        foreach (ToolAction actions in toolActions)     // check tool action in list
        {
            if(actions.isApplied)   //  if one of the tool action in list is successfully applied
            {
                actions.isApplied = false;  // toggle to false to prevent loop
                return true;
            }
        }

        return false;
    }
}
