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
        taskConditionUI.text = "(" + taskCurrentProgress.ToString() + "/" + taskToCompleteProgress.ToString() + ")";

        // Condition
        if(semai.isApplied)
        {
            Debug.Log("Semai called");
            semai.isApplied = false;

            foreach (Vector3Int brokenMapCoord in brokenMapList)
            {
                if (mapManager.GetTileBase(brokenMapCoord) == null)
                {
                    if(!isComplete)
                        taskCurrentProgress++;
                    brokenMapList.Remove(brokenMapCoord);
                    Debug.Log("coord remoced");
                }
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
        brokenMapList.Clear();

        taskNameUI.color = Color.white;
        taskConditionUI.color = Color.white;

        if (ToolsTilesTracker.pakans.Count >= taskToCompleteProgress)
        {
            taskCurrentProgress = 0;
            for (int i = 0; i < taskToCompleteProgress; i++)
            {
                int rand = 0;
                do
                {
                    rand = Random.Range(0, ToolsTilesTracker.pakans.Count);
                }
                while (mapManager.baseMap.GetTile(ToolsTilesTracker.pakans[rand]) == brokenMap);

                brokenMapList.Add(ToolsTilesTracker.pakans[rand]);
                mapManager.baseMap.SetTile(ToolsTilesTracker.pakans[rand], brokenMap);
                mapManager.cropsManager.crops[(Vector2Int)ToolsTilesTracker.pakans[rand]].isAbnormal = true;
            }
        }
        else
            taskCurrentProgress = taskToCompleteProgress;   // cancel activation by completing
    }

}
