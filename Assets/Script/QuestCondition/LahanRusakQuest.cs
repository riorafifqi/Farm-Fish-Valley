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

        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";

        foreach (Vector3Int brokenMapCoord in brokenMapList)
        {
            if(mapManager.GetTileBase(brokenMapCoord) == fixedMap)
            {
                if (!isComplete)
                    taskCurrentProgress++;
                brokenMapList.Remove(brokenMapCoord);
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)
        {
            isComplete = true;
        }
        else
            isComplete = false;

        if (isComplete)
        {
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.plows.Count;
        foreach (Vector3Int plow in ToolsTilesTracker.plows)
        {
            brokenMapList.Add(plow);
        }

        Debug.Log("TTTCount: " + ToolsTilesTracker.pakans.Count);
        Debug.Log("brokenMapListCount: " + brokenMapList.Count);

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);
        }
    }

    private void OnDisable()
    {
        brokenMapList.Clear();
    }



}
