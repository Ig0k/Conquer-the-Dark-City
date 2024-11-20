using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtSceneManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Base")
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
