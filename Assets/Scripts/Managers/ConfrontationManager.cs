using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfrontationManager : MonoBehaviour
{
    [SerializeField] GameObject _enemy1, _enemy2, _enemy3;
    [SerializeField] private GameObject[] _divisbleEnemies;
    [SerializeField] CharacterData _characterData;

    [SerializeField] private int _index = 0;

    private void Awake()
    {
        if(_characterData == null) _characterData = FindObjectOfType<CharacterData>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Confrontation1"))
        {
            if (_enemy1 == null && _enemy2 == null && _enemy3 == null)
            {
                _characterData.confrontationWinned = true;
                SceneManager.LoadScene("Map");
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Confrontation2"))
        {
            if (_enemy1 == null && _enemy2 == null)
            {
                _characterData.confrontationWinned = true;
                SceneManager.LoadScene("Map");

                //UnityEngine.Cursor.lockState = CursorLockMode.None;
                //UnityEngine.Cursor.visible = false;
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Confrontation3"))
        {
            if (_index < _divisbleEnemies.Length)
            {
                if (_divisbleEnemies[_index].activeSelf == false)
                {
                    _index++;
                }                  
            }
            else
            {
                SceneManager.LoadScene("Map");
            }
        }
    }
}
