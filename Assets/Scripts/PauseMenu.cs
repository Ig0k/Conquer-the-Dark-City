using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!gamePaused)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) gamePaused = true;
        }
        else if(gamePaused)
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

    public void OpenPauseMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        UnityEngine.Cursor.visible = true;
    }

    public void ClosePauseMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        UnityEngine.Cursor.visible = false;
    }
}
