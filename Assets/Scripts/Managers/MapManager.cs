using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string _sceneToGo;

    [SerializeField] private string _zone;

    [SerializeField] private GameObject _charSelectionUI;

    [SerializeField] private int _chancesToConfrontation = 0;
    [SerializeField] private GameObject _confrontationWarning;

    public void SaveSceneString(string zone)
    {
        _zone = zone;
    }

    public void GoToScene()
    {
        _chancesToConfrontation = Random.Range(0, 11);
        if (_chancesToConfrontation <= 3)
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

        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene("Confrontation1");
    }
}
