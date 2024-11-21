using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyDivisible : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _damage = 2;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _minDistanceToAttack = 10f, _minDistanceForWps = 5, _timeBetweenWps = 2f;

    [SerializeField] private Transform[] _wayPoints;

    private int i = 0;

    private void Awake()
    {
        if(_agent == null) _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        StartCoroutine(Patroll());
    }

    private IEnumerator Patroll()
    {
        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(_wayPoints[i].position);

            if (Vector2.Distance(transform.position, _wayPoints[i].position) <= _minDistanceForWps)
            {
                i++;
                yield return new WaitForSeconds(_timeBetweenWps);

            }
            if (i >= _wayPoints.Length - 1)
            {
                i = 0;
            }
        }
    }

    public void Divide()
    {
        Debug.Log("Divide");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.GetComponent<PlayerLife>();
            playerLife.Life -= _damage;
        }
    }

}
