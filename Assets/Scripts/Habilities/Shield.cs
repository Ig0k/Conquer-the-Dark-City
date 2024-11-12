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

    [SerializeField] private Enemy2[] _enemy2Script;
    [SerializeField] private EnemyPrototype[] _enemy1Script;
    [SerializeField] private NewEnemy1[] _newEnemys1Script;
    [SerializeField] private Enemy3[] _enemy3Script;

    [SerializeField] private float _knockbakcForce = 3, _knockbackDuration = 0.23f;
    [SerializeField] private int _damage = 0;

    [SerializeField] private float _distanceToKnockback = 7f;

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
     

        if (!_onCD)
        {
            for (int i = 0; i < _enemy2Script.Length; i++)
            {
                if (_enemy2Script[i] != null)
                {
                    float distance = Vector2.Distance(transform.position, _enemy2Script[i].transform.position);

                    if (_enemy2Script[i] != null && distance <= _distanceToKnockback)
                    {
                        _enemy2Script[i].TakeDamage(_damage, _knockbakcForce, _knockbackDuration); //el tercer parámetro, el newPunchCD, debe ser mayor, no menor (al reves que los otros 2)
                    }
                }
            }
            for (int i = 0; i < _enemy1Script.Length; i++)
            {
                if (_enemy1Script[i] != null)
                {
                    float distance = Vector2.Distance(transform.position, _enemy1Script[i].transform.position);

                    if (_enemy1Script[i] != null && distance <= _distanceToKnockback)
                    {
                        _enemy1Script[i].TakeDamage(_damage, _knockbakcForce, _knockbackDuration);
                    }
                }
            }
            for (int i = 0; i < _newEnemys1Script.Length; i++)
            {
                if(_newEnemys1Script[i] != null)
                {
                    float distance = Vector2.Distance(transform.position, _newEnemys1Script[i].transform.position);

                    if (distance <= _distanceToKnockback)
                    {
                        _newEnemys1Script[i].TakeDamage(_damage, _knockbakcForce, _knockbackDuration);
                    }
                }
            }
            for (int i = 0; i < _enemy3Script.Length; i++)
            {
                if (_enemy3Script[i] != null)
                {
                    float distance = Vector2.Distance(transform.position, _enemy3Script[i].transform.position);

                    if (_enemy3Script[i] != null && distance <= _distanceToKnockback)
                    {
                        _enemy3Script[i].TakeDamage(_damage, _knockbakcForce, _knockbackDuration);
                    }
                }
            }
        }
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
