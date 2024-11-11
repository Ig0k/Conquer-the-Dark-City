using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        gamePaused = false;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Base")
        {
            if (!gamePaused)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) gamePaused = true;
            }
            else if (gamePaused)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) gamePaused = false;
            }

            if (gamePaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
        
    }

    public void OpenPauseMenu()
    {
        if(SceneManager.GetActiveScene().name != "Base")
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            UnityEngine.Cursor.visible = true;
        }   
    }

    public void ClosePauseMenu()
    {
        if (SceneManager.GetActiveScene().name != "Base")
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;

            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}
