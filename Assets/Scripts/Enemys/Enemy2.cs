using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private int _life = 5;

    [SerializeField] private int _maxLife = 20;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _minDistanceForWps = 0.2f;
    [SerializeField] private float _minDistanceToFollowPlayer = 4f;
    [SerializeField] private float _minDistanceToPunchPlayer = 2f;
    [SerializeField] private float _minDistanceToStop;

    [SerializeField] private bool _isFollowingPlayer = false;

    [SerializeField] private int _wpsIndex = 0;

    [Header("Punch References")]
    [SerializeField] private GameObject _Punch;
    //[SerializeField] private Bullet _bulletScript;
    [SerializeField] private Transform _sight;

    [SerializeField] private float _PunchCD = 1f;
    [SerializeField] private bool _canPunch = true;

    [Header("Punch Properties")]
    [SerializeField] private float _PunchSpeed = 15f;
    [SerializeField] private float _PunchDestroyTime = 5f;
    [SerializeField] private int _PunchDamage = 3;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
                if (_distanceFromPlayer <= _minDistanceToPunchPlayer && _canPunch)
                {

                    StartCoroutine(Punch());

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

    private IEnumerator Punch()
    {
        _canPunch = false;
        Instantiate(_Punch, transform.position, transform.rotation);
        yield return new WaitForSeconds(_PunchCD);
        _canPunch = true;
    }

    public int TakeDamage(int damage)
    {
        _life -= damage;
        return _life;
    }


}