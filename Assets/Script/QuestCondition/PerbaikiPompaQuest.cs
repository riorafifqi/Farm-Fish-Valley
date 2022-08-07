using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerbaikiPompaQuest : Quest
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

        taskToCompleteProgress = ToolsTilesTracker.pompas.Count;
        foreach (Vector3Int pompa in ToolsTilesTracker.pompas)
        {
            brokenMapList.Add(pompa);
        }

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);
        }
    }



}
