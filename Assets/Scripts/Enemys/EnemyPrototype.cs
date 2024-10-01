using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPrototype : MonoBehaviour
{
    [SerializeField] private int _life = 5;

    [SerializeField] private int _maxLife = 20;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _minDistanceForWps = 0.2f;
    [SerializeField] private float _minDistanceToFollowPlayer = 15f;
    [SerializeField] private float _minDistanceToShootPlayer = 15f;
    [SerializeField] private float _minDistanceToStop;

    [SerializeField] private bool _isFollowingPlayer = false;

    [SerializeField] private int _wpsIndex = 0;

    [Header("Bullet References")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;
    [SerializeField] private Transform _sight;

    [SerializeField] private float _shootCD = 1f;
    [SerializeField] private bool _canShoot = true;

    [Header("Bullet Properties")]
    [SerializeField] private float _bulletSpeed = 15f;
    [SerializeField] private float _bulletDestroyTime = 5f;
    [SerializeField] private int _bulletDamage = 1;

    private float _ogBulletSpeed = 0f;
    private float _ogBulletDestroyTime = 0f;
    private float _ogMoveSpeed = 0f;
    private float _ogShootCD = 0f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _ogBulletSpeed = _bulletSpeed;
        _ogBulletDestroyTime = _bulletDestroyTime;
        _ogMoveSpeed = _agent.speed;
        _ogShootCD = _shootCD;

        if(_bulletScript != null) _bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);

    }

    public void TimeModification(float newSpeed, float newBulletSpeed, float newBulletDestroyTime, 
        float newShootCD)
    {    
        _bulletSpeed = newBulletSpeed;
        _bulletDestroyTime = newBulletDestroyTime;
        _shootCD = newShootCD;

        _bulletScript.SetProperties(newSpeed, newBulletDestroyTime, _bulletDamage);
        if(_agent.speed != 0) _agent.speed = newSpeed;
    }

    public void BackToOgParams()
    {
        _bulletSpeed = _ogBulletSpeed;
        _bulletDestroyTime = _ogBulletDestroyTime;
        _agent.speed = _ogMoveSpeed;
        _shootCD = _ogShootCD;

        _bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);
    }

    private void Start()
    {
        //ESTO SIRVE PARA QUE EL PERSONAJE NO DESAPAREZCA, POR NO ENTRAR EN MAS DETALLES

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        //_bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);
    }

    private void Update()
    {
        if (_life <= 0) Destroy(gameObject);

        _life = Mathf.Clamp(_life, 0, _maxLife);

        float _distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (gameObject != null)
        {
            if (_agent.isOnNavMesh)
            {


                if (_isFollowingPlayer && _distanceFromPlayer >= _minDistanceToStop)
                {
                    _agent.isStopped = false;
                    FollowPlayer(_playerTransform);
                }
                else if (_isFollowingPlayer && _distanceFromPlayer < _minDistanceToStop)
                {
                    _agent.isStopped = true;

                }
                else if (_isFollowingPlayer == false && _agent.isStopped == false)
                {
                    Patroll();
                }

                //SHOOT
                if (_distanceFromPlayer <= _minDistanceToShootPlayer && _canShoot)
                {

                    StartCoroutine(Shoot());

                }

                //FOLLOW PLAYER
                if (_distanceFromPlayer <= _minDistanceToFollowPlayer)
                {
                    _isFollowingPlayer = true;
                }
                else _isFollowingPlayer = false;

            }
        }

    }

    private void FollowPlayer(Transform player)
    {
        if (_agent.isOnNavMesh)
        {
            Vector2 dirToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);
            transform.rotation = lookRotation;

            _agent.SetDestination(player.position);
        }
    }

    private void Patroll()
    {
        if (_agent.isOnNavMesh)
        {
            Vector2 dirToPlayer = (_wayPoints[_wpsIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);
            transform.rotation = lookRotation;

            _agent.SetDestination(_wayPoints[_wpsIndex].position);

            if (Vector2.Distance(transform.position, _wayPoints[_wpsIndex].position) <= _minDistanceForWps)
            {
                _wpsIndex++;
            }
            if (_wpsIndex >= _wayPoints.Length)
            {
                _wpsIndex = 0;
            }
        }
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        Instantiate(_bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }

    public int TakeDamage(int damage)
    {
        _life -= damage;
        return _life;
    }

    public void Die() { Destroy(gameObject); }

}
