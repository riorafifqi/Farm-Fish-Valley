using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject toolbarUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InventoryToggler();
    }

    public void Resume()
    {
        if(isPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void Pause()
    {
        if(!isPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void InventoryToggler()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            toolbarUI.SetActive(true);
        }
        else if (!inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
            toolbarUI.SetActive(false);
        }
    }


}
