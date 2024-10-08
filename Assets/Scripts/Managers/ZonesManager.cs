using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZonesManager : MonoBehaviour
{
    [SerializeField] private int _zone1Percentaje, _zone2Percentaje, _zone3Percentaje;
    [SerializeField] private TMP_Text _zone1, _zone2, _zone3;

    [SerializeField] private Button _CH1Button, _CH2Button;

    [SerializeField] private CharacterData _characterData;

    [SerializeField] private GameObject _winnedConfrontationUI;

    [SerializeField] private PlayerUpgrades _upgrades;


    private void Awake()
    {
        if(_characterData == null) _characterData = FindObjectOfType<CharacterData>();

        if (_characterData.confrontationWinned == true)
        {
            _upgrades.level = 1; //el nivel inicial es 0. Si ganamos un enfrentamiento, sube a 1
            CharacterData.character1Level = 1;
            CharacterData.character2Level = 1;
            _winnedConfrontationUI.SetActive(true);
        }
    }


    private void Update()
    {
        _zone1.text = _zone1Percentaje.ToString() + "%";
        _zone2.text = _zone2Percentaje.ToString() + "%";
        _zone3.text = _zone3Percentaje.ToString() + "%";

        _CH1Button.onClick.AddListener(ChangeCharacterTo1);
        _CH2Button.onClick.AddListener(ChangeCharacterTo2);

        if (_characterData._zone1CH1 == true) _zone1Percentaje = 100;
        else if (_characterData._zone1CH2 == true) _zone1Percentaje = 100;
    }

    public void CloseConfrontationWindow()
    {
        _winnedConfrontationUI.SetActive(false);
    }

    private void ChangeCharacterTo1()
    {
        CharacterData._character = 1;      
    }
    private void ChangeCharacterTo2()
    {
        CharacterData._character = 2;
    }

    

}
