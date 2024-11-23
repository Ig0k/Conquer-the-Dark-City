using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone6Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesToSpawn;
    [SerializeField] private float _timeForFirstWave = 5f;

    private void Start()
    {
        StartCoroutine(FirstWaveSpawner());
    }

    private IEnumerator FirstWaveSpawner()
    {
        yield return new WaitForSeconds(_timeForFirstWave);
        _enemiesToSpawn[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        _enemiesToSpawn[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        _enemiesToSpawn[2].SetActive(true);
    }
}
