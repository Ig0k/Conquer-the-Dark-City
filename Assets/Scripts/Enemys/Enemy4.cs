using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy4 : MonoBehaviour
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

    [SerializeField] private Invisibility _invisibility;

    [SerializeField] private Rigidbody2D _rb;

    [Header("Knockback Values")]
    [SerializeField] private float _knockbackForce = 2f, _knockBackDuration;
    private Coroutine knockbackCoroutine;

    [Header("Sounds")]
    [SerializeField] private AudioClip _shootClip, _impactClip;
    [SerializeField] private SoundsManager _audioManager;

    private int _wayPointToGo = 0;
    [SerializeField] private float _timeBetweenTps = 2f;
    private bool _canTp = true;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _ogBulletSpeed = _bulletSpeed;
        _ogBulletDestroyTime = _bulletDestroyTime;
        _ogMoveSpeed = _agent.speed;
        _ogShootCD = _shootCD;

        if (_bulletScript != null) _bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();

        if (_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    private void Update()
    {
        float _distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (!_isFollowingPlayer)
        {
            if(_canTp) StartCoroutine(Patroll());
        }
        else
        {
            if (_canTp) StartCoroutine(Patroll());
            Shoot();
        }
    }

    private IEnumerator Patroll()
    {
        _canTp = false;

        yield return new WaitForSeconds(_timeBetweenTps);
        _wayPointToGo = Random.Range(0, _wayPoints.Length);
        transform.position = _wayPoints[_wayPointToGo].position;

        _canTp = true;
    }

    private void Shoot()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
    }
}
