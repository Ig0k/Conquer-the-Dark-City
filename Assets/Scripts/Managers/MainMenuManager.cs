using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    [SerializeField] private GameObject _soundsMenu, _mainMenu;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ActiveSoundPreferencesMenu()
    {
        _soundsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void ActiveMainMenu()
    {
        _soundsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
