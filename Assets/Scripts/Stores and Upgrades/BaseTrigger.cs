using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseTrigger : MonoBehaviour
{
    [SerializeField] private bool _canShowPanel = false;

    [SerializeField] private GameObject[] _panels;
    [SerializeField] private int _panelToShow;

    [SerializeField] private GameObject _eKeyText;

    [SerializeField] private Cursor _cursorScript;

    [SerializeField] private bool _isMapTrigger = false;

    private void Start()
    {
        Time.timeScale = 1f;

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7) //player layer
        {
            _canShowPanel = true;

            _eKeyText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //player layer
        {
            _canShowPanel = false;

            if (_eKeyText != null)
            {
                _eKeyText.SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (_panels[_panelToShow].activeSelf == false && _canShowPanel && Input.GetKeyDown(KeyCode.E))
        {
            _panels[_panelToShow].SetActive(true);

            Time.timeScale = 0f;
            _cursorScript.enabled = false;

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

            _eKeyText.SetActive(false);
        }
        else if (_isMapTrigger && _panels[_panelToShow].activeSelf == true && _canShowPanel && Input.GetKeyDown(KeyCode.E))
        {
            _panels[_panelToShow].SetActive(false);

            Time.timeScale = 1f;
            _cursorScript.enabled = true;

            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        
    }

    public void CloseMenu()
    {
        _panels[_panelToShow].SetActive(false);

        Time.timeScale = 1f;
        _cursorScript.enabled = true;

        //UnityEngine.Cursor.visible = false;
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
