using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave2Trigger : MonoBehaviour
{
    [SerializeField] private Zone0Manager _zone0Script;

    [SerializeField] private GameObject[] _cameras; //0 = original, 1 = waves2

    [SerializeField] private bool _changeCamera = false;

    private void Awake()
    {
        _zone0Script = FindObjectOfType<Zone0Manager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if (_zone0Script.wave1Winned && !_changeCamera)
            {
                _cameras[0].SetActive(false);
                _cameras[1].SetActive(true);

                _changeCamera = true;
            }
            else if (_zone0Script.wave1Winned && _changeCamera)
            {
                _cameras[0].SetActive(true);
                _cameras[1].SetActive(false);

                _changeCamera = false;
            }
        }
        
    }
}
