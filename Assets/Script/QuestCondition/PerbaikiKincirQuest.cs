using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerbaikiKincirQuest : Quest
{
    MapManager mapManager;
    public TileBase brokenMap;
    public TileBase fixedMap;
    public List<Vector3Int> brokenMapList;

    private void Awake()
    {
        mapManager = GameObject.Find("GameManager").GetComponent<MapManager>();
    }

    private void Start()
    {
        //brokenMapList = new List<Vector3Int>();

        taskNameUI.text = taskName;
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";
    }

    private void Update()
    {
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";    // update progress UI

        foreach (Vector3Int brokenMapCoord in brokenMapList)        // check all broken kincir in field
        {
            if(mapManager.GetTileBase(brokenMapCoord) == fixedMap)      // if kincir tileMap back to normal
            {
                if (!isComplete)
                    taskCurrentProgress++;      // increment progress
                brokenMapList.Remove(brokenMapCoord);       // remove fixed tileMap coord from list
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if task current progress equal to target
        {
            isComplete = true;      // complete task
        }
        else
            isComplete = false;

        if (isComplete)
        {
            // set task UI color to complete color
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        // set default color and progress on enable
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.kincirs.Count;       // target based on kincir in field
        foreach (Vector3Int kincir in ToolsTilesTracker.kincirs)        // add all kincir to potential broken list
        {
            brokenMapList.Add(kincir);
        }

        for (int i = 0; i < brokenMapList.Count; i++)       // set all kincir in field to broken kincir
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);
        }
    }



}
