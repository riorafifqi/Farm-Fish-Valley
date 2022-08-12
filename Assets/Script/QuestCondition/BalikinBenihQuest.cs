using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BalikinBenihQuest : Quest
{
    MapManager mapManager;
    public TileBase brokenMap;
    public TileBase fixedMap;
    public List<Vector3Int> brokenMapList;

    public TilePickUp pickUp;
    public ToolAction semai;
    public float balikinInProgress;

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
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";    // update progress

        // Condition
        if(semai.isApplied)     // if semai successfully applied
        {
            Debug.Log("Semai called");
            semai.isApplied = false;    // to prevent loop

            foreach (Vector3Int brokenMapCoord in brokenMapList)    // check crops origin
            {
                if (mapManager.GetTileBase(brokenMapCoord) == null)     // if there's no crops in the origin map
                {
                    if(!isComplete) 
                        taskCurrentProgress++;      // update progress
                    brokenMapList.Remove(brokenMapCoord);   // remove coord from list
                    Debug.Log("coord remoced");
                }
            }
        }

        if (taskCurrentProgress == taskToCompleteProgress)      // if target achieved
        {
            isComplete = true;  // complete task
        }
        else
            isComplete = false;

        if (isComplete)     // if complete
        {
            // set UI text color to complete color
            taskNameUI.color = completeColor;
            taskConditionUI.color = completeColor;
        }
    }

    private void OnEnable()
    {
        brokenMapList.Clear();      // clear all list

        // set default color
        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;

        if (ToolsTilesTracker.pakans.Count >= taskToCompleteProgress)       // if crops to move is more than target
        {
            taskCurrentProgress = 0;        // set progress to 0
            for (int i = 0; i < taskToCompleteProgress; i++)    // based on target, randomize crops in field to move
            {
                int rand = 0;
                do
                {
                    rand = Random.Range(0, ToolsTilesTracker.pakans.Count);
                }
                while (mapManager.baseMap.GetTile(ToolsTilesTracker.pakans[rand]) == brokenMap);    // prevent system randomize same crops

                brokenMapList.Add(ToolsTilesTracker.pakans[rand]);      // add randomize crops
                mapManager.baseMap.SetTile(ToolsTilesTracker.pakans[rand], brokenMap);      // set crops sprite to abnormal
                mapManager.cropsManager.crops[(Vector2Int)ToolsTilesTracker.pakans[rand]].isAbnormal = true;    // set crops status to abnormal
            }
        }
        else
            taskCurrentProgress = taskToCompleteProgress;   // cancel activation by completing the task
    }

}
