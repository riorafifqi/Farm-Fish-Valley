using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] SwipeMenuAnimation[] levelSelectPanels;
    int selectedPanel;
    [SerializeField] GameObject nextButton, prevButton;

    public Button[] udangLvlButtons;
    public Button[] nilaLvlButtons;
    public Button[] bandengLvlButtons;

    // Start is called before the first frame update
    void Start()
    {
        selectedPanel = 0;

        // Level Udang
        int udangLevelAt = PlayerPrefs.GetInt("udangLevelAt", 1);
        for (int i = 0; i < udangLvlButtons.Length; i++)        // make all locked level uninteractable
        {
            if (i + 1 > udangLevelAt)
                udangLvlButtons[i].interactable = false;
        }

        // Level Nila
        int nilaLevelAt = PlayerPrefs.GetInt("nilaLevelAt", 4);
        for (int i = 0; i < nilaLvlButtons.Length; i++)     // make all locked level uninteractable
        {
            if (i + 4 > nilaLevelAt)
                nilaLvlButtons[i].interactable = false;
        }

        // Level Bandeng
        int bandengLevelAt = PlayerPrefs.GetInt("bandengLevelAt", 7);
        for (int i = 0; i < bandengLvlButtons.Length; i++)      // make all locked level uninteractable
        {
            if (i + 7 > bandengLevelAt)
                bandengLvlButtons[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // control previous and next button
        if (selectedPanel == 0)     // if panel is in first slide
            prevButton.SetActive(false);
        else if (selectedPanel == levelSelectPanels.Length - 1)     // panel is in last slide
            nextButton.SetActive(false);
        else  // panel in the middle
        {
            nextButton.SetActive(true);
            prevButton.SetActive(true);
        }
    }

    public void NextPanel()
    {
        if (selectedPanel < levelSelectPanels.Length - 1)
        {
            selectedPanel++;
            levelSelectPanels[selectedPanel].SwipeLeftIn();        // Object in from left to mid
            levelSelectPanels[selectedPanel - 1].SwipeLeftOut();  // Object out from mid to right
        }
    }

    public void PreviousPanel()
    {
        if (selectedPanel > 0)
        {
            selectedPanel--;
            levelSelectPanels[selectedPanel].SwipeRightIn();       // Object in from the left
            levelSelectPanels[selectedPanel + 1].SwipeRightOut();  // Object out to the left
        }
    }

    public void LevelSelect(int levelSelect)
    {
        SceneManager.LoadScene(levelSelect);
    }
}
