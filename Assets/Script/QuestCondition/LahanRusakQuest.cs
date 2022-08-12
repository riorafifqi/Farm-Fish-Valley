using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LahanRusakQuest : Quest
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
        if (taskToCompleteProgress > ToolsTilesTracker.plows.Count)
            taskToCompleteProgress = ToolsTilesTracker.plows.Count;

        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";      // update text UI

        foreach (Vector3Int brokenMapCoord in brokenMapList)        // check all brokenMap in field
        {
            if(mapManager.GetTileBase(brokenMapCoord) == fixedMap)  // if brokenMap change to fixedMap
            {
                if (!isComplete)
                    taskCurrentProgress++;
                brokenMapList.Remove(brokenMapCoord);       // remove fixedMap from brokenMap list
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if target fulfilled
        {
            isComplete = true;      // complete task
        }
        else
            isComplete = false;

        if (isComplete)
        {
            // change task ui color to completeColor
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        // set UI and progress to default
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.plows.Count;     // target based on all plowed map
        foreach (Vector3Int plow in ToolsTilesTracker.plows)        // add all plowed map to broken list
        {
            brokenMapList.Add(plow);
        }

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);        // change plowed map to broken map
        }
    }

    private void OnDisable()
    {
        brokenMapList.Clear();      // clear brokenMap after task finish
    }



}
