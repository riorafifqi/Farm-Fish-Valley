using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemContainer itemContainer;
    public DayTimeController timeController;
    public ItemDragNDropController dragNDropController;
    private StartPanelAnimation autoScroll;

    public Tilemap environmentTilemap;
    private QuizManager quiz;

    private void Awake()
    {
        quiz = GameObject.Find("Quiz").GetComponent<QuizManager>();
        instance = this;
        environmentTilemap = GameObject.Find("Grid").transform.GetChild(2).GetComponent<Tilemap>();
        dragNDropController = gameObject.GetComponent<ItemDragNDropController>();
        autoScroll = GameObject.FindObjectOfType<StartPanelAnimation>();
    }

    private void Start()
    {
        autoScroll.Open();  // Open start panel
    }
}
