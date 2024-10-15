using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject[] _texts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            StartCoroutine(FinishLevel());
        }
    }

    private IEnumerator FinishLevel()
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        _texts[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        _texts[1].SetActive(true);
        yield return new WaitForSeconds(2f);
        _texts[2].SetActive(true);
        yield return new WaitForSeconds(3f);
        _gameManager.WinLevel(1);
        Destroy(gameObject);
    }
}
