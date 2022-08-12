using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;
    int currentSelectedTool;

    private void Awake()
    {
        toolbarController = GameObject.Find("GameManager").GetComponent<ToolbarController>();
    }

    private void Start()
    {
        Init();
        toolbarController.onChange += Highlight;       // highlight first item
        Highlight(0);   
    }

    private void Update()
    {
        Show();     // update toolbar UI
    }

    public override void OnClick(int id)
    {
        toolbarController.Set(id);  // choose used item to clicked toolbar button
        Highlight(id);  // highlight selected toolbar button
    }

    public void Highlight (int id)
    {
        buttons[currentSelectedTool].Highlight(false);  // un-highlight previous button
        currentSelectedTool = id;   // select new button
        buttons[currentSelectedTool].Highlight(true);   // highlight new button
    }
}
