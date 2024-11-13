using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string _sceneToGo;

    [SerializeField] private string _zone;

    [SerializeField] private GameObject _charSelectionUI;

    public int chancesToConfrontation = 0;
    [SerializeField] private GameObject _confrontationWarning;

    [Header("Bribe Settings")]

    [SerializeField] private int _moneyToBribe = 5;
    [SerializeField] private bool _bribedZone0 = false, _bribedZone5 = false, _bribedZone3 = false;
    [SerializeField] private GameObject _bribeFeedback, _bribeText;

    [SerializeField] private TMP_Text _moneyUI;

    //[Header("Button Visuals")]
    //[SerializeField] private Image _zone0Image;
    //[SerializeField] private Sprite _zone0ClickedImage, _zone0Deselected;

    public void SaveSceneString(string zone)
    {
        _zone = zone;
    }

    private void Update()
    {
        _moneyUI.text = "$ " + Money.money;
    }

    private void Start()
    {
        //_bribedZone0 = false;
        //_bribedZone5 = false;
        //_bribedZone3 = false;

        if (SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "Art")
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None; 
            UnityEngine.Cursor.visible = true;
        }

        Time.timeScale = 1f;

        //_zone0Deselected = _zone0Image.sprite;
    }

    public void Bribe()
    {
        if(_zone == "Zone 0")
        {
            BribeZone0();
        }
        else if(_zone == "Zone 5")
        {
            BribeZone5();
        }
        else if(_zone == "Zone 3")
        {
            BribeZone3();
        }
        else
        {
            Debug.Log(1);
        }
    }

    public void BribeZone0() //bribe == sobornar
    {
        if(Money.money >= _moneyToBribe)
        {
            _bribedZone0 = true;
            _confrontationWarning.SetActive(false);

            Money.money -= _moneyToBribe;
        }
        StartCoroutine(BribeUI());
        StopCoroutine(GoToConfrontation());
    }

    public void BribeZone5() //bribe == sobornar
    {
        if (Money.money >= _moneyToBribe)
        {
            _bribedZone5 = true;
            _confrontationWarning.SetActive(false);

            Money.money -= _moneyToBribe;
        }
        StartCoroutine(BribeUI());
        StopCoroutine(GoToConfrontation());
    }

    public void BribeZone3() //bribe == sobornar
    {
        if (Money.money >= _moneyToBribe)
        {
            _bribedZone3 = true;
            _confrontationWarning.SetActive(false);

            Money.money -= _moneyToBribe;
        }
        StartCoroutine(BribeUI());
        StopCoroutine(GoToConfrontation());
    }

    private IEnumerator BribeUI()
    {
        _bribeFeedback.SetActive(true);
        yield return new WaitForSeconds(1f);
        _bribeText.SetActive(true);
        yield return new WaitForSeconds(3.7f);     
        _bribeText.SetActive(false);
        yield return new WaitForSeconds(5f);
        _bribeFeedback.SetActive(false);
    }

    //public void ImageOnClick()
    //{
    //    _zone0Image.sprite = _zone0ClickedImage;
    //}

    //public void ImageDeselected()
    //{
    //    _zone0Image.sprite = _zone0Deselected;
    //}

    public void GoToScene()
    {
        chancesToConfrontation = Random.Range(0, 12);

        if(_zone == "Zone 0")
        {
            if(chancesToConfrontation <= 4 && !_bribedZone0)
            {
                StartCoroutine(GoToConfrontation());
            }
            else if(_bribedZone0 || chancesToConfrontation > 4)
            {
                SceneManager.LoadScene(_zone);
            }
        }
        else if (_zone == "Zone 5")
        {
            if (chancesToConfrontation <= 4 && !_bribedZone5)
            {
                StartCoroutine(GoToConfrontation());
            }
            else if(_bribedZone5 || chancesToConfrontation > 4)
            {
                SceneManager.LoadScene(_zone);
            }
        }
        else if (_zone == "Zone 3")
        {
            if (chancesToConfrontation <= 4 && !_bribedZone3)
            {
                StartCoroutine(GoToConfrontation());
            }
            else if (_bribedZone3 || chancesToConfrontation > 4)
            {
                SceneManager.LoadScene(_zone);
            }
        }
    }

    public void GoToBase()
    {
        SceneManager.LoadScene("Base");
    }

    public void ShowCharacterSelection()
    {
        _charSelectionUI.SetActive(true);
    }


    private IEnumerator GoToConfrontation()
    {
        if(_zone == "Zone 0" && !_bribedZone0)
        {
            _confrontationWarning.SetActive(true);

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("Confrontation1");
        }
        else if (_zone == "Zone 5" && !_bribedZone5)
        {
            _confrontationWarning.SetActive(true);

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("Confrontation1");
        }
        else if (_zone == "Zone 3" && !_bribedZone3)
        {
            _confrontationWarning.SetActive(true);

            yield return new WaitForSeconds(4f);

            SceneManager.LoadScene("Confrontation1");
        }

    }
}
