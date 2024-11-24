using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone3Manager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _moneyAtFinsihLevel = 50;

    [SerializeField] private FlagsZone3[] _flags;

    private void Update()
    {
        if (_flags[0].flagConquisted && _flags[1].flagConquisted)
        {
            _gameManager.WinLevel(3);
        }
    }
}
