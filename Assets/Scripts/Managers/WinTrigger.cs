using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject[] _texts;

    [SerializeField] private PlayerLife _playerLife;

    [SerializeField] private int _moneyAtFinsihLevel = 50;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private PlayerWeapon _playerWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Money.money += _moneyAtFinsihLevel;

            _playerLife.enabled = false;
            _pauseMenu.enabled = false;
            _playerWeapon.enabled = false;
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
