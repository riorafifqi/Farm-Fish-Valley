using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SupplemenQuest : Quest
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

        taskNameUI.text = taskName;     // assign task name to UI
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";    // assign progress to UI
    }

    private void Update()
    {
        if (taskToCompleteProgress > ToolsTilesTracker.pakans.Count)
            taskToCompleteProgress = ToolsTilesTracker.pakans.Count;    

        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";       // update progress to UI

        foreach (Vector3Int brokenMapCoord in brokenMapList)    // check all unhealthy crops in tilemap
        {
            if(mapManager.cropsManager.crops[(Vector2Int)brokenMapCoord].isHealthy)     // if one of the crops in list turn healthy
            {
                if (!isComplete)
                    taskCurrentProgress++;      // update progress
                brokenMapList.Remove(brokenMapCoord);   // remove healthy crops from unhealthy list
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if condition fulfilled, complete task
        {
            isComplete = true;
        }
        else
            isComplete = false;

        if (isComplete)
        {
            taskNameUI.color = completeColor;           // change color when complete
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        // set color and score to default when enabled
        taskNameUI.color = Color.white;         
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.pakans.Count;        // target all pakaned crops as condition
        foreach (Vector3Int pakan in ToolsTilesTracker.pakans)
        {
            brokenMapList.Add(pakan);       // add all pakaned crops to unhealthy list
        }

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            //mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);
            mapManager.cropsManager.crops[(Vector2Int)brokenMapList[i]].isHealthy = false;      // set all tile in list to unhealthy
        }
    }



}
