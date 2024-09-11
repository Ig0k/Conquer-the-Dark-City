using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance;
    [SerializeField] private bool _isPlayer = false;

    public void SetProperties( float destroyTime, int damage)
    {
        
        _destroyTime = destroyTime;
        _damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.TryGetComponent<PlayerLife>(out PlayerLife playerLife);
            if (playerLife.parpadeo)
            {
                playerLife.StartCoroutine("Parpadeo");
            }

            playerLife.Life -= _damage;
        }
    }


    private void Update()
    {
     

        Destroy(gameObject, _destroyTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, _rayDistance, _layerMask);

        if (_isPlayer)
        {
            if (hit.collider != null && hit.collider.TryGetComponent<EnemyPrototype>(out EnemyPrototype enemy1))
            {
                enemy1.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
        /*
        else
        {
            if (hit.collider != null && hit.collider.TryGetComponent<PlayerLife>(out PlayerLife playerLife))
            {
                if (playerLife.parpadeo) playerLife.Life -= _damage;

                playerLife.ShowParticles();

                if (playerLife.parpadeo)
                {
                    playerLife.StartCoroutine("Parpadeo");
                    Debug.Log("1");
                }

                Destroy(gameObject);
            }
        }
        */

    }
}
