using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _knockbackForce = 1.15f, _knockbackDuration = 0.1f;

    [SerializeField] private bool _isPlayer = false;

    public void SetProperties(float speed, float destroyTime, int damage)
    {
        _speed = speed;
        _destroyTime = destroyTime;
        _damage = damage;
    }


    private void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;

        Destroy(gameObject, _destroyTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 0.5f, _layerMask);
        RaycastHit2D hitWalls = Physics2D.Raycast(transform.position, transform.up, 0.5f);

        if (hitWalls.collider != null && hitWalls.collider.gameObject.layer == 8) // layer 8 = Wall
        {
            Destroy(gameObject);
            Debug.Log("Colisionando con pared");
        }
  
        if (_isPlayer)
        {
            if (hit.collider != null && hit.collider.TryGetComponent<EnemyPrototype>(out EnemyPrototype enemy1))
            {
                enemy1.TakeDamage(_damage, _knockbackForce, _knockbackDuration);
                Destroy(gameObject);
            }
            if (hit.collider != null && hit.collider.TryGetComponent<Enemy3>(out Enemy3 enemy3))
            {
                enemy3.TakeDamage(_damage, _knockbackForce, _knockbackDuration);
                Destroy(gameObject);
            }
            if (hit.collider != null && hit.collider.TryGetComponent<Enemy2>(out Enemy2 enemy2))
            {
                enemy2.TakeDamage(_damage, _knockbackForce, _knockbackDuration);
                Destroy(gameObject);
            }
            if (hit.collider !=null && hit.collider.TryGetComponent<Torreta>(out Torreta turret))
            {
                turret.TakeDamage(_damage);
                Destroy(gameObject);

            }
            if (hit.collider!= null && hit.collider.TryGetComponent<Kamikaze>(out Kamikaze kam))
            {
                kam.TakeDamage(_damage);
                Destroy(gameObject);
            }
            if (hit.collider != null && hit.collider.TryGetComponent<Enemy4>(out Enemy4 enemy4))
            {
                enemy4.TakeDamage(_damage);
                Destroy(gameObject);
            }
            if (hit.collider != null && hit.collider.TryGetComponent<NewEnemy1>(out NewEnemy1 newEnemy1))
            {
                newEnemy1.TakeDamage(_damage, _knockbackForce, _knockbackDuration);
                Destroy(gameObject);
            }
            if(hit.collider != null && hit.collider.TryGetComponent<EnemyDivisible>(out EnemyDivisible enemyDivi))
            {
                enemyDivi.Divide();
                Destroy(gameObject);
            }
        }

        else
        {
            if (hit.collider != null && hit.collider.TryGetComponent<PlayerLife>(out PlayerLife playerLife))
            {
                //if(playerLife.parpadeo) playerLife.Life -= _damage;
                if (playerLife.enabled)
                {
                    playerLife.Life -= _damage;
                    playerLife.ShakeCall();

                    Destroy(gameObject);
                }
 
            }
        } 

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) //layer wall
        {
            Destroy(gameObject);
        }
    }
}
