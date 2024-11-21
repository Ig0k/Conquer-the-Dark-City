using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class newGrenade : MonoBehaviour
{
    [SerializeField] private GameObject _grenade;
    [SerializeField] private int _grenadesLeft = 5;
    [SerializeField] private Transform _mousePos;

    private bool _onCD = false;
    [SerializeField] private float _maxCD = 3f;
    private float _currentCD = 0f;

    [HideInInspector] public Vector2 mousePos;

    private void Update()
    {
        mousePos = (transform.position - _mousePos.position).normalized;
        
        Timer();

        if (Input.GetKeyDown(KeyCode.G) && !_onCD && _grenadesLeft > 0)
        {
            ThrowGrenade();
            _onCD = true;
        }
    }

    private void ThrowGrenade()
    {
        Instantiate(_grenade, transform.position, transform.rotation);
        _grenadesLeft--;
    }

    private void Timer()
    {
        if (_onCD)
        {
            _currentCD += Time.deltaTime;

            if (_currentCD > _maxCD)
            {
                _onCD = false;
                _currentCD = 0f;
            }
        }
    }
}
