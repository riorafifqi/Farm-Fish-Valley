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
    public GameObject autoScroll;

    public Tilemap environmentTilemap;
    public QuizManager quiz;

    private void Awake()
    {
        quiz = GameObject.Find("Quiz").GetComponent<QuizManager>();
        instance = this;
        environmentTilemap = GameObject.Find("Grid").transform.GetChild(2).GetComponent<Tilemap>();
        dragNDropController = gameObject.GetComponent<ItemDragNDropController>();
        autoScroll = GameObject.Find("AutoRollTextPanel");
    }

    private void Start()
    {
        autoScroll.GetComponent<Animator>().SetBool("IsOpen", true);
        //quiz.gameObject.SetActive(false);
    }
}
