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
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";    // update UI

        foreach (Vector3Int brokenMapCoord in brokenMapList)        // get all broken pompa in field
        {
            if(mapManager.GetTileBase(brokenMapCoord) == fixedMap)  // if broken pompa map change to fixed pompa map
            {
                if (!isComplete)
                    taskCurrentProgress++;      // inncrement progress
                brokenMapList.Remove(brokenMapCoord);   // remove fixed pompa from list
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if target achieved
        {
            isComplete = true;      // complete task
        }
        else
            isComplete = false;

        if (isComplete)     // if complete
        {
            // change UI text color to complete color
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        // set color and progress to default onEnable
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;
        taskCurrentProgress = 0;

        taskToCompleteProgress = ToolsTilesTracker.pompas.Count;    // target based on available pompa on field
        foreach (Vector3Int pompa in ToolsTilesTracker.pompas)      
        {
            brokenMapList.Add(pompa);   // add all available pompa on field
        }

        for (int i = 0; i < brokenMapList.Count; i++)
        {
            mapManager.baseMap.SetTile(brokenMapList[i], brokenMap);    // set all avaliable pompa on field sprite to broken
        }
    }
}
