using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradesStore : MonoBehaviour
{
    [Header("Store Properties")]

    [SerializeField] private int _invisibilityPrice = 30;
    [SerializeField] private int _shootBoostPrice = 80;
    [SerializeField] private int _masiveShootPrice = 10;

    public static bool invisibilityBought = false;
    public static bool shootBoostBought = false;
    public static bool masiveShootBought = false;

    [SerializeField] private TMP_Text _moneyText;

    [Header("Trigger Properties")]

    [SerializeField] private bool _canShowPanel = false;

    [SerializeField] private GameObject[] _panels;
    [SerializeField] private int _panelToShow;

    [SerializeField] private GameObject _eKeyText;

    [SerializeField] private Cursor _cursorScript;

    [SerializeField] private bool _isMapTrigger = false;

    [Header("Sounds")]

    [SerializeField] private AudioClip _boughtClip;
    [SerializeField] private SoundsManager _audioManager;

    private void Awake()
    {
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    public void BuyMasiveShoot()
    {
        if (CharacterData._character == 1 && Money.money >= _masiveShootPrice && !masiveShootBought)
        {
            masiveShootBought = true;

            Money.money -= _masiveShootPrice;

            _audioManager.PlaySound(_boughtClip, 5f);

            PowerManagement.canUseMasiveShoot = true;
        }
    }

    public void BuyInvisibility()
    {
        if(Money.money >= _invisibilityPrice && !invisibilityBought)
        {
            invisibilityBought = true;

            Money.money -= _invisibilityPrice;

            _audioManager.PlaySound(_boughtClip, 5f);
        }
    }

    public void BuyShootBoost()
    {
        if (Money.money >= _shootBoostPrice && !shootBoostBought)
        {
            shootBoostBought = true;

            Money.money -= _shootBoostPrice;

            _audioManager.PlaySound(_boughtClip, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //player layer
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

        _moneyText.text = "Money: " + Money.money.ToString();
    }

    public void CloseMenu()
    {
        _panels[_panelToShow].SetActive(false);

        Time.timeScale = 1f;
        _cursorScript.enabled = true;

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
}
