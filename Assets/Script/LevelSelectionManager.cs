using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelSelectPanels;
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
        for (int i = 0; i < udangLvlButtons.Length; i++)
        {
            if (i + 1 > udangLevelAt)
                udangLvlButtons[i].interactable = false;
        }

        // Level Nila
        int nilaLevelAt = PlayerPrefs.GetInt("nilaLevelAt", 4);
        for (int i = 0; i < nilaLvlButtons.Length; i++)
        {
            if (i + 4 > nilaLevelAt)
                nilaLvlButtons[i].interactable = false;
        }

        // Level Bandeng
        int bandengLevelAt = PlayerPrefs.GetInt("bandengLevelAt", 7);
        for (int i = 0; i < bandengLvlButtons.Length; i++)
        {
            if (i + 7 > bandengLevelAt)
                bandengLvlButtons[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // control previous and next button
        if (selectedPanel == 0)
            prevButton.SetActive(false);
        else if (selectedPanel == levelSelectPanels.Length - 1)
            nextButton.SetActive(false);
        else
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
            levelSelectPanels[selectedPanel].SetActive(true);
            levelSelectPanels[selectedPanel - 1].SetActive(false);
        }
    }

    public void PreviousPanel()
    {
        if (selectedPanel > 0)
        {
            selectedPanel--;
            levelSelectPanels[selectedPanel].SetActive(true);
            levelSelectPanels[selectedPanel + 1].SetActive(false);
        }
    }

    public void LevelSelect(int levelSelect)
    {
        SceneManager.LoadScene(levelSelect);
    }
}
