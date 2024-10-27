using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float _shieldDuration = 7f, _shieldCD = 0f;
    [SerializeField] private bool _onCD = false, _startCD = false, _canUseShield = true;

    [SerializeField] private float _timeBetween = 5.5f;

    public delegate void ShieldDelegate();
    public ShieldDelegate shield = delegate { };

    [SerializeField] private GameObject _shieldGameObject;
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Awake()
    {
        if(_playerLife == null) _playerLife = GetComponent<PlayerLife>();
        if(_playerMovement == null) _playerMovement = GetComponent<PlayerMovement>();

        if (CharacterData._character == 2)
        {
            if (PowerManagement.canUseShield) enabled = true;
            else enabled = false;
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (_startCD) Timer();
        else _startCD = false;

        //if (_startCD && !_onCD) _canFreezeTime = true;

        shield();

        if (_canUseShield && Input.GetKeyDown(KeyCode.F) && _onCD)
        {
            shield = ShieldMethod;
            _startCD = true;
            _shieldCD = 0f;

            StartCoroutine(StartShield());
        }
        else if (!_onCD)
        {
            shield = NoShield;
            _onCD = true;

            _startCD = true;
        }
    }

    private IEnumerator StartShield()
    {
        _canUseShield = false;

        yield return new WaitForSeconds(_timeBetween);

        _canUseShield = true;
    }

    private void ShieldMethod()
    {
        _shieldGameObject.SetActive(true);
        _playerLife.enabled = false;
        _playerMovement.enabled = false;
    }

    private void NoShield()
    {
        _shieldGameObject.SetActive(false);
        _playerLife.enabled = true;
        _playerMovement.enabled = true;
    }

    private void Timer()
    {
        if (_onCD)
        {
            _shieldCD += Time.deltaTime;

            if (_shieldCD > _shieldDuration)
            {
                _onCD = false;
                _shieldCD = 0f;
            }
        }
    }

    
}
