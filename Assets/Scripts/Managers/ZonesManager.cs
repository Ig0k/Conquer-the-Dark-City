using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZonesManager : MonoBehaviour
{
    [SerializeField] private int _zone1Percentaje, _zone2Percentaje, _zone3Percentaje;
    [SerializeField] private TMP_Text _zone1, _zone2, _zone3;

    [SerializeField] private Button _CH1Button, _CH2Button;

    private void Update()
    {
        _zone1.text = _zone1Percentaje.ToString() + "%";
        _zone2.text = _zone2Percentaje.ToString() + "%";
        _zone3.text = _zone3Percentaje.ToString() + "%";

        _CH1Button.onClick.AddListener(ChangeCharacterTo1);
        _CH2Button.onClick.AddListener(ChangeCharacterTo2);
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
