using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlameForEnemy : MonoBehaviour
{
    [SerializeField] private float _maxCD = 0.4f;
    private float _currentCD = 0f;
    private bool _onCD = false;

    [SerializeField] private int _damage = 1;

    private void Update()
    {
        if (_onCD)
        {
            _currentCD += Time.deltaTime;

            if (_currentCD >= _maxCD)
            {
                _onCD = false;
                _currentCD = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerLife>(out PlayerLife playerLife))
        {
            if (!_onCD)
            {
                if (collision.TryGetComponent<PlayerLife>(out PlayerLife player))
                {
                    player.Life -= _damage;
                    playerLife.ShakeCall();
                }
                _onCD = true;
            }

        }
    }
}
