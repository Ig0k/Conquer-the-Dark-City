using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public static CharacterData Instance;

    public static int _character = 1;
    [SerializeField] private int _characterVisualization = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActiveCharacter(int character)
    {
        _character = character;
    }

    private void Update()
    {
        _characterVisualization = _character;
    }
}
