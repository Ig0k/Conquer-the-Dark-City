using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    [SerializeField] private float _freezeDuration = 7f, _freezeCD = 0f;
    [SerializeField] private bool _onCD = false, _startCD = false, _canFreezeTime = true;

    [SerializeField] private float _timeBetween = 5.5f;

    public delegate void TimeFreezeDelegate();
    public TimeFreezeDelegate timeFreeze = delegate { };

    [SerializeField] private Enemy2[] _enemy2Script;
    [SerializeField] private EnemyPrototype[] _enemy1Script;

    [SerializeField] private GameObject _UIFreezeEffect;

    private void Awake()
    {
        if(PowerManagement.canUseTimeFreeze) enabled = true;
        else enabled = false;
    }

    private void Update()
    {
        if (_startCD) Timer();
        else _startCD = false;

        //if (_startCD && !_onCD) _canFreezeTime = true;

        timeFreeze();

        if (_canFreezeTime && Input.GetKeyDown(KeyCode.Q) && _onCD)
        {
            timeFreeze = FreezeTimeMethod;
            _startCD = true;
            _freezeCD = 0f;

            StartCoroutine(StartTimeFreeze());
        }
        else if (!_onCD)
        {
            timeFreeze = NormalTime;
            _onCD = true;

            _startCD = true;
        }

        
    }

    private IEnumerator StartTimeFreeze()
    {
        _canFreezeTime = false;

        yield return new WaitForSeconds(_timeBetween); 

        _canFreezeTime = true;
    }

    private void FreezeTimeMethod()
    {
        //Debug.Log("Tiempo Freezado");
        //escribir la logica de freezeo de tiempo aca

        _UIFreezeEffect.SetActive(true);

        for (int i = 0; i < _enemy2Script.Length; i++)
        {
            if(_enemy2Script[i] != null)
            {
                _enemy2Script[i].TimeModification(5, 2f, 3f); //el tercer parámetro, el newPunchCD, debe ser mayor, no menor (al reves que los otros 2)
            }
            
        }
        for (int i = 0; i < _enemy1Script.Length; i++)
        {
            if(_enemy1Script[i] != null)
            {
                _enemy1Script[i].TimeModification(1.2f, 13f, 12f, 3f);
            }
            
        }
          
    }

    private void NormalTime()
    {
        //Debug.Log("Tiempo normal");
        //escribir logica de tiempo no freezado aca

        _UIFreezeEffect.SetActive(false);

        for (int i = 0; i < _enemy2Script.Length; i++)
        {
            if (_enemy2Script[i] != null)
            {
                _enemy2Script[i].BackToOgParams();
            } 
        }

        for (int i = 0; i < _enemy1Script.Length; i++)
        {
            if (_enemy1Script[i] != null)
            {
                _enemy1Script[i].BackToOgParams();
            }
            
        }
    }

    private void Timer()
    {
        if (_onCD)
        {
            _freezeCD += Time.deltaTime;

            if (_freezeCD > _freezeDuration)
            {
                _onCD = false;
                _freezeCD = 0f;
            }
        }
    }
}
