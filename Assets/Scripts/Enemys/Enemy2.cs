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
    [SerializeField] private SpriteRenderer _spriteRenderer;

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

    public float _ogSpeed = 0, _ogPunchSpeed = 0, _ogPunchCD = 0;

    [SerializeField] private Invisibility _invisibility;

    [SerializeField] private Rigidbody2D _rb;

    [Header("Knockback Values")]
    [SerializeField] private float _knockbackForce = 2f, _knockBackDuration;
    private Coroutine knockbackCoroutine;

    [Header("Sounds")]
    [SerializeField] private AudioClip _impactClip;
    [SerializeField] private SoundsManager _audioManager;

    [SerializeField] private ParticleSystem _bloodParticles;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _ogSpeed = _agent.speed;
        _ogPunchSpeed = _PunchSpeed;
        _ogPunchCD = _PunchCD;

        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TimeModification(float newPunchSpeed, float newSpeed, float newPunchCD)
    {
        _PunchSpeed = newPunchSpeed;
        _agent.speed = newSpeed;
        _PunchCD = newPunchCD;
    }

    public void BackToOgParams()
    {
        _PunchCD = _ogPunchCD;
        _agent.speed = _ogSpeed;
        _PunchSpeed = _ogPunchSpeed;
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

        Vector2 dirToPlayer = (_playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);

        if (gameObject != null)
        {
            if (_agent.isOnNavMesh)
            {
                if (_invisibility.isInvisible)
                {                  
                    _isFollowingPlayer = false;
                    _agent.isStopped = false;

                    Patroll();
                }
                if (_isFollowingPlayer && _distanceFromPlayer >= _minDistanceToStop 
                    && _invisibility.isInvisible == false)
                {
                    _agent.isStopped = false;
                    FollowPlayer(_playerTransform, lookRotation);
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
                if (_distanceFromPlayer <= _minDistanceToPunchPlayer && _canPunch 
                    && _invisibility.isInvisible == false)
                {
                    StartCoroutine(Punch());
                }

                //FOLLOW PLAYER
                if (_distanceFromPlayer <= _minDistanceToFollowPlayer && _invisibility.isInvisible == false)
                {
                    _isFollowingPlayer = true;
                }
                else if(_distanceFromPlayer > _minDistanceToFollowPlayer || _invisibility.isInvisible)
                {
                    _isFollowingPlayer = false;
                }

                if (_isFollowingPlayer)
                {
                    transform.rotation = lookRotation;
                }
            }
            
        }

    }

    private void FollowPlayer(Transform player, Quaternion rot)
    {
        if (_agent.isOnNavMesh)
        {
            transform.rotation = rot;

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

    public int TakeDamage(int damage, float knockbackForce, float knockbackDur)
    {
        _life -= damage;

        _audioManager.PlaySound(_impactClip, 0.35f);

        Vector2 dirToPlayer = transform.position - _playerTransform.position;

        Instantiate(_bloodParticles, transform.position, transform.rotation);

        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);

        StartCoroutine(SpriteDamaged());

        knockbackCoroutine = StartCoroutine(Knockback(dirToPlayer, knockbackForce, knockbackDur));

        return _life;
    }

    private IEnumerator Knockback(Vector2 dir, float knockbackForce, float knockbackDur)
    {
        _rb.velocity = dir * knockbackForce;

        yield return new WaitForSeconds(knockbackDur);

        _rb.velocity = Vector2.zero;
    }

    private IEnumerator SpriteDamaged()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.08f);
    }

    public void Die() { Destroy(gameObject); }

}