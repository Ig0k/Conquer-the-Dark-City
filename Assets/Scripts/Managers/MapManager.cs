using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string _sceneToGo;

    [SerializeField] private string _zone;

    [SerializeField] private GameObject _charSelectionUI;

    public int chancesToConfrontation = 0;
    [SerializeField] private GameObject _confrontationWarning;

    [SerializeField] private int _moneyToBribe = 5;
    private bool _bribed = false;

    public void SaveSceneString(string zone)
    {
        _zone = zone;
    }

    private void Start()
    {
        _bribed = false;

        if(SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "Art")
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None; 
            UnityEngine.Cursor.visible = true;
        }

        Time.timeScale = 1f;
    }

    public void Bribe() //bribe == sobornar
    {
        if(Money.money >= _moneyToBribe)
        {
            _bribed = true;
            _confrontationWarning.SetActive(false);

            Money.money -= _moneyToBribe;
        }
    }

    public void GoToScene()
    {
        chancesToConfrontation = Random.Range(0, 12);
        if (chancesToConfrontation <= 4 && !_bribed)
        {
            StartCoroutine(GoToConfrontation());
        }
        else
        {
            SceneManager.LoadScene(_zone);
        }
        
    }

    public void ShowCharacterSelection()
    {
        _charSelectionUI.SetActive(true);
    }


    private IEnumerator GoToConfrontation()
    {
        _confrontationWarning.SetActive(true);

        yield return new WaitForSeconds(4f);

        if(!_bribed)
        {
            SceneManager.LoadScene("Confrontation1");
        }
    }
}
