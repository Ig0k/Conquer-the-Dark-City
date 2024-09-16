using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfrontationManager : MonoBehaviour
{
    [SerializeField] GameObject _enemy1, _enemy2, _enemy3;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Confrontation"))
        {
            if(_enemy1 == null && _enemy2 == null && _enemy3 == null)
            {
                SceneManager.LoadScene("Map");
            }
        }
    }
}
