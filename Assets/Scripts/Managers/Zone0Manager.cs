using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone0Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _wave1; //primeros enemigos
    public bool wave1Winned = false;

    [SerializeField] private GameObject[] _mapLimit;

    int i = 0;

    private void Update()
    {
        i = 0;

        for (int index = 0; index < _wave1.Length; index++)
        {
            if (_wave1[index] == null)
            {
                i++;
            }
        }

        if (wave1Winned)
        {
            _mapLimit[0].SetActive(false);
        }

        if (i >= _wave1.Length) wave1Winned = true;
    }
}
