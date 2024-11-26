using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class newGrenadeExplotion : MonoBehaviour
{
    [SerializeField] private float _maxCD = 0.4f;
    private float _currentCD = 0f;
    private bool _onCD = false;

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _exploitionDuration = 4f;

    [SerializeField] private bool _isPlayer = false;

    private void Start()
    {
        Destroy(gameObject, _exploitionDuration);
    }

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
        if (_isPlayer)
        {
            if (collision.TryGetComponent<EnemyPrototype>(out EnemyPrototype proto)
           || collision.TryGetComponent<Enemy2>(out Enemy2 enemy2) ||
           collision.TryGetComponent<Enemy3>(out Enemy3 enemy3) ||
           collision.TryGetComponent<NewEnemy1>(out NewEnemy1 new1)
               || collision.TryGetComponent<FireEnemy>(out FireEnemy fire) ||
               collision.TryGetComponent<Boss>(out Boss finalBoss))
            {
                if (!_onCD)
                {
                    if (collision.TryGetComponent<EnemyPrototype>(out EnemyPrototype prototype))
                    {
                        prototype.TakeDamage(_damage, 0f, 0f);
                    }
                    if (collision.TryGetComponent<Enemy2>(out Enemy2 en2))
                    {
                        en2.TakeDamage(_damage, 0f, 0f);
                    }
                    if (collision.TryGetComponent<NewEnemy1>(out NewEnemy1 newEnemy1))
                    {
                        newEnemy1.TakeDamage(_damage, 0f, 0f);
                    }
                    if (collision.TryGetComponent<Enemy3>(out Enemy3 en3))
                    {
                        en3.TakeDamage(_damage, 0f, 0f);
                    }
                    if (collision.TryGetComponent<FireEnemy>(out FireEnemy enemyFire))
                    {
                        enemyFire.TakeDamage(_damage);
                    }
                    if(collision.TryGetComponent<Boss>(out Boss boss))
                    {
                        boss.TakeDamage(_damage);
                    }

                    _onCD = true;
                }

            }
            
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!_onCD)
                {
                    collision.TryGetComponent<PlayerLife>(out PlayerLife playerLife);
                    playerLife.TakeDamage(_damage);
                    playerLife.ShakeCall();

                    _onCD = true;
                }
            }
        }
    }
}
