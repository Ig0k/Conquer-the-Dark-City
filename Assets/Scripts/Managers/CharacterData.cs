using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterData : MonoBehaviour
{
    public static CharacterData Instance;

    public static int _character = 1;
    [SerializeField] private int _characterVisualization = 1;

    public bool _zone1CH1 = false, _zone2CH1 = false, _zone3CH1 = false;
    public bool _zone1CH2 = false, _zone2CH2 = false, _zone3CH2 = false;//Zonas completadas o no completadas

    public bool confrontationWinned = false;

    public static int character1Level = 0;
    public static int character2Level = 0;

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

        Debug.Log(character1Level + " + " + character2Level);
    }

    public void ActiveCharacter(int character)
    {
        _character = character;
    }

    private void Update()
    {
        _characterVisualization = _character;

        //EN BASE:
        if(SceneManager.GetActiveScene().name == "Base")
        {
            if (Input.GetKeyDown(KeyCode.Tab) && _character == 1 && Input.GetAxis("Horizontal") == 0 
                && Input.GetAxis("Vertical") == 0)
            {
                _character = 2;
            }
            else if(Input.GetKeyDown(KeyCode.Tab) && _character == 2 && Input.GetAxis("Horizontal") == 0
                && Input.GetAxis("Vertical") == 0)
            {
                _character = 1;
            }
        }
    }

    public void CompletedZones(int zoneCompleted)
    {
        if(_character == 1)
        {
            if (zoneCompleted == 1)
            {
                _zone1CH1 = true;
            }
            else if (zoneCompleted == 2)
            {
                _zone1CH1 = true;
            }
            else if (zoneCompleted == 3)
            {
                _zone1CH1 = true;
            }
        }
        else if (_character == 2)
        {
            if (zoneCompleted == 1)
            {
                _zone1CH2 = true;
            }
            else if (zoneCompleted == 2)
            {
                _zone1CH2 = true;
            }
            else if (zoneCompleted == 3)
            {
                _zone1CH2 = true;
            }
        }
    }
}
