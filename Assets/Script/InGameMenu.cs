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
            Time.timeScale = 1f;    // unpause
            isPaused = false;
        }
    }

    public void Pause()
    {
        if(!isPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;    // pause
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

    public void InventoryOpen()
    {
        inventoryUI.transform.localPosition = new Vector2(0f, -Screen.height);
        inventoryUI.GetComponentInChildren<CanvasGroup>().alpha = 0f;

        inventoryUI.GetComponentInChildren<CanvasGroup>().LeanAlpha(1f, 0.5f);
        inventoryUI.transform.LeanMoveLocalY(0f, 0.5f);
        toolbarUI.transform.LeanMoveLocalY(-Screen.height, 0.5f);
    }

    public void InventoryClose()
    {
        inventoryUI.transform.localPosition = new Vector2(0f, 0f);

        inventoryUI.GetComponentInChildren<CanvasGroup>().LeanAlpha(0f, 0.5f);
        inventoryUI.transform.LeanMoveLocalY(-Screen.height, 0.5f);
        toolbarUI.transform.LeanMoveLocalY(59.97f, 0.5f);
    }


}
