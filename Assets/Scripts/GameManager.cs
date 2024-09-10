using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private string _currentSceneName;

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

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_playerLife.Life <= 0)
        {
            StartCoroutine(LoadCurrentScene());
        }
    }

    private IEnumerator LoadCurrentScene()
    {
        _animator.Play("Fade In");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_currentSceneName);
    }
}
