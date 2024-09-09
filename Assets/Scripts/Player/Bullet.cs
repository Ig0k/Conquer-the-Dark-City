using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;

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

        if (_isPlayer)
        {
            if (hit.collider != null && hit.collider.TryGetComponent<EnemyPrototype>(out EnemyPrototype enemy1))
            {
                enemy1.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (hit.collider != null && hit.collider.TryGetComponent<PlayerLife>(out PlayerLife playerLife))
            {
                if(playerLife.parpadeo) playerLife.Life -= _damage;

                playerLife.ShowParticles();

                if (playerLife.parpadeo)
                {
                    playerLife.StartCoroutine("Parpadeo");
                    Debug.Log("1");
                }         

                Destroy(gameObject);
            }
        }

    }
}
