using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PakanKurangQuest : Quest
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

        // assign task name and progress to UI
        taskNameUI.text = taskName;
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";
    }

    private void Update()
    {
        if (taskToCompleteProgress > ToolsTilesTracker.pakans.Count)        // if crops is ready to harvest
            taskToCompleteProgress = ToolsTilesTracker.pakans.Count;        // and player harvest the crops

        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";    // update progress

        foreach (Vector3Int brokenMapCoord in brokenMapList)        // check all un-pakan-ed coord in list
        {
            if(mapManager.GetTileBase(brokenMapCoord) == fixedMap)  // if the tile in coordinate in list is re-pakan-ed
            {
                if (!isComplete)        // if not complete
                    taskCurrentProgress++;      // update progress
                brokenMapList.Remove(brokenMapCoord);   // remove re-pakan-ed tile in list
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if progress equal to condition to complete
        {
            isComplete = true;      // complete the task
        }
        else
            isComplete = false;

        if (isComplete)     // if task complete
        {
            // change UI color to complete color
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        // when enabled, set color and progress to default
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.pakans.Count;        // condition based on all pakan-ed crops in field
        foreach (Vector3Int pakan in ToolsTilesTracker.pakans)      // add all pakan-ed crops to potential un-pakan-ed list
        {
            brokenMapList.Add(pakan);
        }

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);    // change all pakan-ed crops in potential un-pakan-ed list to un-pakan-ed
        }
    }



}
