using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Panen Quest")]
public class PanenQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        if(harvest.isApplied)
        {
            if (!cropTile.crop.name.Contains("Semai"))
            {
                cropTile = null;
                harvest.isApplied = false;
                return true;
            }
        }

        return false;
    }

}
