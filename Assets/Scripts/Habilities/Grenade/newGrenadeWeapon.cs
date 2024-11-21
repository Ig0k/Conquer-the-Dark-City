using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class newGrenadeWeapon : MonoBehaviour
{
    [SerializeField] private float _grenadeForce = 5f;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _explotion;
    [SerializeField] private float _destroyTime = 2f;
    [SerializeField] private newGrenade _grenadeThrowScript;

    private bool _canExplode = false;

    private void Awake()
    {
        if( _rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_grenadeThrowScript == null) _grenadeThrowScript = FindObjectOfType<newGrenade>();
    }

    private void Start()
    {
        _rb.AddForce(-_grenadeThrowScript.mousePos * _grenadeForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        _destroyTime -= Time.deltaTime;

        if(_destroyTime <= 0 || _canExplode)
        {
            Explode(); 
        }
    }

    private void Explode()
    {
        Instantiate(_explotion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.TryGetComponent<EnemyPrototype>(out EnemyPrototype proto)
            || collision.TryGetComponent<Enemy2>(out Enemy2 enemy2) ||
            collision.TryGetComponent<Enemy3>(out Enemy3 enemy3) ||
            collision.TryGetComponent<NewEnemy1>(out NewEnemy1 new1))
        {
            _canExplode = true;
        }
    }
}
