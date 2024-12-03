using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Singleton
    public static PauseMenu instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PauseMenu found!");
            return;
        }
        instance = this;
    }

    #endregion

    public bool paused = false;

    public Canvas pauseMenu;
    public Canvas inventoryMenu;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        paused = true;

        pauseMenu.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;

        paused = false;

        pauseMenu.gameObject.SetActive(false);
        inventoryMenu.gameObject.SetActive(false);
    }
}
