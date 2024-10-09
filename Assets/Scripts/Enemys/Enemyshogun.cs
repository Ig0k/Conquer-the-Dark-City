using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemyshogun : MonoBehaviour
{
    [SerializeField] private int _life = 5;
    [SerializeField] private int _maxLife = 20;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _playerTransform;

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

    
    [SerializeField] private int _bulletCount = 3; 
    [SerializeField] private float _spreadAngle = 15f;

    private float _ogBulletSpeed = 0f;
    private float _ogBulletDestroyTime = 0f;
    private float _ogMoveSpeed = 0f;
    private float _ogShootCD = 0f;

    [SerializeField] private Invisibility _invisibility;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _ogBulletSpeed = _bulletSpeed;
        _ogBulletDestroyTime = _bulletDestroyTime;
        _ogMoveSpeed = _agent.speed;
        _ogShootCD = _shootCD;

        if (_bulletScript != null) _bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);
    }

    public void TimeModification(float newSpeed, float newBulletSpeed, float newBulletDestroyTime,
        float newShootCD)
    {
        _bulletSpeed = newBulletSpeed;
        _bulletDestroyTime = newBulletDestroyTime;
        _shootCD = newShootCD;

        _bulletScript.SetProperties(newSpeed, newBulletDestroyTime, _bulletDamage);
        if (_agent.speed != 0) _agent.speed = newSpeed;
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
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (_life <= 0) Destroy(gameObject);

        _life = Mathf.Clamp(_life, 0, _maxLife);

        float _distanceFromPlayer = Vector2.Distance(transform.position, _playerTransform.transform.position);

        if (gameObject != null)
        {
            if (_agent.isOnNavMesh)
            {
                if (_invisibility != null && _invisibility.isInvisible)
                {
                    _isFollowingPlayer = false;
                    _agent.isStopped = false;

                    Patroll();
                }

                if (_isFollowingPlayer && _distanceFromPlayer >= _minDistanceToStop)
                {
                    _agent.isStopped = false;
                    FollowPlayer(_playerTransform.transform);
                }
                else if (_isFollowingPlayer && _distanceFromPlayer < _minDistanceToStop
                    && _invisibility.isInvisible == false)
                {
                    _agent.isStopped = true;
                }
                else if (_isFollowingPlayer == false && _agent.isStopped == false)
                {
                    Patroll();
                }

                // SHOOT
                if (_distanceFromPlayer <= _minDistanceToShootPlayer && _canShoot
                    && _invisibility.isInvisible == false)
                {
                    StartCoroutine(Shoot());
                }

                // FOLLOW PLAYER
                if (_distanceFromPlayer <= _minDistanceToFollowPlayer)
                {
                    _isFollowingPlayer = true;
                }
                else if (_invisibility.enabled && _invisibility.isInvisible == true)
                {
                    _isFollowingPlayer = false;
                }
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

        for (int i = 0; i < _bulletCount; i++)
        {
            // Calcular el ángulo de dispersión
            float angle = (i - (_bulletCount - 1) / 2f) * _spreadAngle;

            // Instanciar la bala
            GameObject bulletInstance = Instantiate(_bullet, _sight.position, Quaternion.identity);

            // Calcular la dirección de la bala con dispersión
            Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * _sight.up;
            bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletDirection * _bulletSpeed;

            // Configurar propiedades de la bala
            bulletInstance.GetComponent<Bullet>().SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);
        }

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
