using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;

    [SerializeField] private int _currentCharacter;
    public static int currentCharacter;

    private void Awake()
    {
        if(_characterData == null)
        {
            _characterData = FindObjectOfType<CharacterData>();
        }
    }

    private void Start()
    {
        if(_characterData != null)
        {
            currentCharacter = CharacterData._character;
            _currentCharacter = currentCharacter;
        }
    }
}
