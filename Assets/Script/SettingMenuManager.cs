using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuManager : MonoBehaviour
{
    [SerializeField] AudioSource mainMenuBGM;
    public Image audioIcon;
    public Sprite turnOffAudio;
    public Sprite turnOnAudio;

    public GameObject settingPanel;
    public GameObject mainPanel;

    public LevelSelectionManager levelSelectionManager;

    public void Awake()
    {
        levelSelectionManager = GameObject.Find("MainMenuManager").GetComponent<LevelSelectionManager>();
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void TurnAudio()
    {
        if (audioIcon.sprite == turnOnAudio)
        {
            audioIcon.sprite = turnOffAudio;
            mainMenuBGM.mute = true;
        }
        else
        {
            audioIcon.sprite = turnOnAudio;
            mainMenuBGM.mute = false;
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("udangLevelAt", 1);
        PlayerPrefs.SetInt("nilaLevelAt", 4);
        PlayerPrefs.SetInt("bandengLevelAt", 7);

        // Level Udang
        int udangLevelAt = PlayerPrefs.GetInt("udangLevelAt", 1);
        for (int i = 0; i < levelSelectionManager.udangLvlButtons.Length; i++)
        {
            if (i + 1 > udangLevelAt)
                levelSelectionManager.udangLvlButtons[i].interactable = false;
        }

        // Level Nila
        int nilaLevelAt = PlayerPrefs.GetInt("nilaLevelAt", 4);
        for (int i = 0; i < levelSelectionManager.nilaLvlButtons.Length; i++)
        {
            if (i + 4 > nilaLevelAt)
                levelSelectionManager.nilaLvlButtons[i].interactable = false;
        }

        // Level Bandeng
        int bandengLevelAt = PlayerPrefs.GetInt("bandengLevelAt", 7);
        for (int i = 0; i < levelSelectionManager.bandengLvlButtons.Length; i++)
        {
            if (i + 7 > bandengLevelAt)
                levelSelectionManager.bandengLvlButtons[i].interactable = false;
        }
    }

}
