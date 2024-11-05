using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public bool isInvisible = false;

    [SerializeField] private float _invisibilityDuration = 7f, _currentCD = 0f;
    [SerializeField] private bool _onCD = false, _startCD = false, _canBeInvisible = true;

    [SerializeField] private float _timeBetween = 5.5f;

    public delegate void InvisibilityPower();
    public InvisibilityPower invisible = delegate { };

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject _UIInvisibleEffect;

    //[SerializeField] private Enemy2[] _enemy2Script;
    //[SerializeField] private EnemyPrototype[] _enemy1Script;

    //[SerializeField] private GameObject _UIFreezeEffect;

    private void Awake()
    {
        if (PowerManagement.canUseInvisibility) enabled = true;
        else enabled = false;
    }

    private void Update()
    {
        if (_startCD) Timer();
        else _startCD = false;

        //if (_startCD && !_onCD) _canFreezeTime = true;

        invisible();

        if (_canBeInvisible && Input.GetKeyDown(KeyCode.Q) && _onCD)
        {
            invisible = InvisibleMethod;
            _startCD = true;
            _currentCD = 0f;

            StartCoroutine(StartInvisiblity());
        }
        else if (!_onCD)
        {
            invisible = Visible;
            _onCD = true;

            _startCD = true;
        }
    }

    private IEnumerator StartInvisiblity()
    {
        _canBeInvisible = false;

        yield return new WaitForSeconds(_timeBetween);

        _canBeInvisible = true;
    }

    private void InvisibleMethod()
    {
        isInvisible = true;

        _UIInvisibleEffect.SetActive(true);

        _spriteRenderer.enabled = false;
    }

    private void Visible()
    {
        isInvisible = false;

        _UIInvisibleEffect.SetActive(false);

        _spriteRenderer.enabled = true;
    }

    private void Timer()
    {
        if (_onCD)
        {
            _currentCD += Time.deltaTime;

            if (_currentCD > _invisibilityDuration)
            {
                _onCD = false;
                _currentCD = 0f;
            }
        }
    }
}
