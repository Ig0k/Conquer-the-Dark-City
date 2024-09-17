using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private string _sceneName, _mapScene;

    [SerializeField] private Animator _animator;

    [SerializeField] private int _currentCharacter;
    public static int currentCharacter;

    private void Awake()
    {
        if(_characterData == null)
        {
            _characterData = FindObjectOfType<CharacterData>();
        }
        if(_playerLife == null)
        {
            _playerLife = FindObjectOfType<PlayerLife>();   
        }

        if (_characterData != null)
        {
            currentCharacter = CharacterData._character;
            _currentCharacter = currentCharacter;
        }
    }

    public void WinLevel(int currentZoneNumb)
    {
        _characterData.CompletedZones(currentZoneNumb);
        StartCoroutine(LoadScene(_mapScene));
    }

    private void Update()
    {
        if (_playerLife.Life <= 0)
        {
            StartCoroutine(LoadScene(_sceneName));
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        _animator.Play("Fade In");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
}