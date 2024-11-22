using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemy : MonoBehaviour
{
    [SerializeField] private int _life = 5;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _minDistanceToThrowFire = 10f, _minDistanceToFollowPlayer, 
        _minDistanceToStop = 4f;
    [SerializeField] private GameObject _fire;

    [SerializeField] private float _rotSpeed = 2f;

    public int TakeDamage(int damage)
    {
        _life -= damage;

        //_audioManager.PlaySound(_impactClip, 0.35f);
        //Instantiate(_bloodParticles, transform.position, transform.rotation);

        //StartCoroutine(SpriteDamaged());

        return _life;
    }

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        Vector2 dirToPlayer = (_playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);

        Vector2 lerpToPlayer = Vector2.Lerp(_playerTransform.position, transform.position, 0.8f).normalized;


        if (_agent.isOnNavMesh)
        {
            if (distanceFromPlayer <= _minDistanceToFollowPlayer && distanceFromPlayer >= _minDistanceToStop)
            {
                _agent.isStopped = false;

                _agent.SetDestination(_playerTransform.position);
                transform.rotation = lookRotation;
            }
            else if(distanceFromPlayer < _minDistanceToStop)
            {
                _agent.isStopped = true;
            }
        }
        
        if(distanceFromPlayer <= _minDistanceToThrowFire)
        {
            ThrowFire();
        }

        if(_life <= 0) Destroy(gameObject);
    }

    private void ThrowFire()
    {
        _fire.SetActive(true);
        _agent.speed = 3f;
    }

    //private void UnableFire()
    //{
    //    _fire.SetActive(false);
    //    _agent.speed = 1.2f;
    //}
}
