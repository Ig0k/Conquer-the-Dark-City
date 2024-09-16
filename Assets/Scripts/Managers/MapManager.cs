using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string _sceneToGo;

    [SerializeField] private string _zone;

    [SerializeField] private GameObject _charSelectionUI;

    public void SaveSceneString(string zone)
    {
        _zone = zone;
    }

    public void GoToScene()
    {
        SceneManager.LoadScene(_zone);
    }

    public void ShowCharacterSelection()
    {
        _charSelectionUI.SetActive(true);
    }
}
