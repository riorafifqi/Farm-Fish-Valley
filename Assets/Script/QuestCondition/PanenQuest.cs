using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Panen Quest")]
public class PanenQuest : QuestCompleteCondition
{
    public override bool Condition()
    {
        if(harvest.isApplied)   // if harvest applied
        {
            if (!cropTile.crop.name.Contains("Semai"))      // if harvested crops is semai, panen task won't increment
            {
                cropTile = null;    // remove crops from tile
                harvest.isApplied = false;      // toggle isApplied back to false to prevent loop
                return true;
            }
        }

        return false;
    }

}
