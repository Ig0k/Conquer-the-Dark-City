using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCharacterChange : MonoBehaviour
{
    //[SerializeField] private GameObject _character1, _character2;

    [SerializeField] private PlayerMovement _player1Movement, _player2Movement;
    [SerializeField] private PlayerAvatar _player1Avatar, _player2Avatar; // para que no roten hacia el mouse

    private void Update()
    {
        if(CharacterData._character == 1)
        {
            _player1Movement.enabled = true;
            _player2Movement.enabled = false;

            _player1Avatar.enabled = true;
            _player2Avatar.enabled = false;
        }
        else if(CharacterData._character == 2)
        {
            _player1Movement.enabled = false;
            _player2Movement.enabled = true;

            _player1Avatar.enabled = false;
            _player2Avatar.enabled = true;
        }
    }
}
