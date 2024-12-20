using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Transform _mousePoint;


    private void Update()
    {
        if (PauseMenu.gamePaused == false || SceneManager.GetActiveScene().name == "Confrontation1")
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
            //transform.up = dir;
            //transform.position = _player.position + new Vector3(0, 1.5f, 0);

            _mousePoint.position = mousePos;

            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        else if(SceneManager.GetActiveScene().name == "Base" ||
            SceneManager.GetActiveScene().name == "Confrontation2" || 
            SceneManager.GetActiveScene().name == "Confrontation3")
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _mousePoint.position = mousePos;
        }

        //if (UnityEngine.Cursor.lockState == CursorLockMode.Confined) Debug.Log("Cursor Confined");
        //else Debug.Log("Cursor NOT confined");
    }
}
