using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header ("Fases")]
    [SerializeField] private int _currentFase = 1;
    [SerializeField] private int _totalFases = 3;
    [SerializeField] private float _fasesDuration = 20f;
    [SerializeField] private float _timeElapsed = 20f;

    [SerializeField] private float _timeBetweenMeleeAttacks = 30f;
    [SerializeField] private float _meleeTimeElapsed = 0f;

    [Header("Life, positions and References")]
    [SerializeField] private int _life = 20;
    [SerializeField] private int _maxLife = 20;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SoundsManager _audioManager;
    [SerializeField] private AudioClip _impactClip;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Transform[] _wayPoints, _sights;
    private bool _canFollowPlayer = false;

    private bool _canChangeWp = true, _canShoot = true;
    [SerializeField] private int _timeBetweenWps = 4, _timeBetweenShoots;

    [SerializeField] private float _minDistanceForWps = 1.5f;
    [SerializeField] private int _wpsIndex;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;

    [Header("Punch")]
    [SerializeField] private GameObject _punch;
    [SerializeField] private Transform _sight;

    [SerializeField] private float _minDistanceToPunch = 3f;

    [SerializeField] private float _PunchCD = 1f;
    [SerializeField] private bool _canPunch = true;

    [SerializeField] private float _PunchSpeed = 15f;
    [SerializeField] private float _PunchDestroyTime = 5f;
    [SerializeField] private int _PunchDamage = 3;

    [Header("Bullet Properties")]
    [SerializeField] private float _bulletSpeed = 15f;
    [SerializeField] private float _bulletDestroyTime = 5f;
    [SerializeField] private int _bulletDamage = 1;

    [Header("Invisible Properties")]
    [SerializeField] private Animator _animator;
    private int _wayPointToGo = 0;
    [SerializeField] private float _timeBetweenTps = 2f;
    private bool _canTp = true, _canBeInvisible;

    private float _generalTimeElapsed = 0f;
    [SerializeField] private float _secondsToSpeedUp = 40f, _secondsToSpeedUp2 = 80f;
    [SerializeField] private float _newBulletSpeed = 10f, _newMoveSpeed = 10;
    [SerializeField] private float _newBulletSpeed2 = 10f, _newMoveSpeed2 = 12;

    [SerializeField] private Image _lifeBar;

    private void Awake()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        if (_animator == null) _animator = GetComponent<Animator>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();

        if (_bulletScript != null) _bulletScript.SetProperties(_bulletSpeed, _bulletDestroyTime, _bulletDamage);     
    }

    private void Start()
    {
        //ESTO SIRVE PARA QUE EL PERSONAJE NO DESAPAREZCA, POR NO ENTRAR EN MAS DETALLES
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _maxLife = _life;
    }

    public int TakeDamage(int damage)
    {
        _life -= damage;

        _audioManager.PlaySound(_impactClip, 0.35f);

        StartCoroutine(SpriteDamaged());

        return _life;
    }

    private IEnumerator SpriteDamaged()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
    }

    private void GeneralTimer()
    {
        _generalTimeElapsed += Time.deltaTime;

        if(_generalTimeElapsed >= _secondsToSpeedUp)
        {
            Debug.Log("First SPEED UP!");
            if (_bulletScript != null) _bulletScript.SetProperties(_newBulletSpeed, _bulletDestroyTime, _bulletDamage);
            _agent.speed = _newMoveSpeed;
        }
        if(_generalTimeElapsed >= _secondsToSpeedUp2)
        {
            Debug.Log("Second SPEED UP!");
            if (_bulletScript != null) _bulletScript.SetProperties(_newBulletSpeed2, _bulletDestroyTime, _bulletDamage);
            _agent.speed = _newMoveSpeed2;
        }
    }

    private void Update()
    {
        if (_life <= 0) SceneManager.LoadScene("Map");

        _lifeBar.fillAmount = (float)_life / _maxLife;

        GeneralTimer();

        Vector2 dirToPlayer = (_playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToPlayer);

        float directionToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        transform.rotation = lookRotation;

        FasesLogic(directionToPlayer);

        if (_canChangeWp && _currentFase != 3) StartCoroutine(GoToWayPoint());

        if (_currentFase != 3) _animator.Play("Base");

        if (_timeElapsed > 0)
        {
            _timeElapsed -= Time.deltaTime;
        }
        else
        {
            _timeElapsed = _fasesDuration;
            if(_currentFase <= _totalFases)
            {
                _currentFase++;
            }
            else
            {
                _currentFase = 1;
            }
        }
    }

    private void FasesLogic(float dirToPlayer)
    {
        if(_meleeTimeElapsed < _timeBetweenMeleeAttacks)
        {
            _meleeTimeElapsed += Time.deltaTime;
        }
        else
        {
            _meleeTimeElapsed = 0f;
            _canFollowPlayer = true;
        }

        if (_canFollowPlayer)
        {
            StartCoroutine(CanFollowPlayer());
        }

        if (dirToPlayer <= _minDistanceToPunch && _canPunch)
        {
            StartCoroutine(MeleeAttack());
        }

        if (_currentFase == 1)
        {
            if (_generalTimeElapsed < _secondsToSpeedUp) _agent.speed = 8;
            else if (_generalTimeElapsed >= _secondsToSpeedUp) _agent.speed = _newMoveSpeed;
            else if (_generalTimeElapsed >= _secondsToSpeedUp2) _agent.speed = _newMoveSpeed2;

            //AGENT SPEED

            if (_canShoot && !_canFollowPlayer) StartCoroutine(Shoot());
            Debug.Log("FASE 1");
        }
        else if(_currentFase == 2)
        {
            if (_canShoot && !_canFollowPlayer) StartCoroutine(TripleShoot());
            Debug.Log("FASE 2");
        }
        else if( _currentFase == 3)
        {
            //LOGICA DE TPS
            if(_canTp) StartCoroutine(PatrollAndShoot());
            Debug.Log("FASE 3");
        }
        else
        {
            _currentFase = 1;
        }

    }
  
    private IEnumerator CanFollowPlayer()
    {
        yield return new WaitForSeconds(10f);
        _canFollowPlayer = false;
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;

        Instantiate(_bullet, transform.position, transform.rotation);

        Debug.Log("Shoot!");

        yield return new WaitForSeconds(_timeBetweenShoots);
        _canShoot = true;
    }

    private IEnumerator MeleeAttack()
    {    
        _canPunch = false;
        
        Debug.Log("Melee Attack!");
        Instantiate(_punch, transform.position, transform.rotation);
        yield return new WaitForSeconds(_PunchCD);

        _canPunch = true;
    }

    private IEnumerator TripleShoot()
    {
        _canShoot = false;

        Instantiate(_bullet, _sights[0].position, _sights[0].rotation);
        Instantiate(_bullet, _sights[1].position, _sights[1].rotation);
        Instantiate(_bullet, _sights[2].position, _sights[2].rotation);

        Debug.Log("Triple Shoot!");

        yield return new WaitForSeconds(_timeBetweenShoots);
        _canShoot = true;
    }

    private IEnumerator GoToWayPoint()
    {
        if (!_canFollowPlayer)
        {
            if (_generalTimeElapsed < _secondsToSpeedUp) _agent.speed = 8;
            else if (_generalTimeElapsed >= _secondsToSpeedUp) _agent.speed = _newMoveSpeed;
            else if (_generalTimeElapsed >= _secondsToSpeedUp2) _agent.speed = _newMoveSpeed2;

            _canChangeWp = false;

            yield return new WaitForSeconds(_timeBetweenWps);

            if (_wpsIndex < _wayPoints.Length)
            {
                _agent.SetDestination(_wayPoints[_wpsIndex].position);
                _wpsIndex = Random.Range(0, _wayPoints.Length - 1);
            }
            //else
            //{
            //    _wpsIndex = 0;
            //}

            _canChangeWp = true;
        }
        else
        {
            _agent.speed = 8;
            _agent.SetDestination(_playerTransform.position);
        }
    }

    private IEnumerator PatrollAndShoot()
    {
        _canTp = false;

        _agent.speed = 0;

        yield return new WaitForSeconds(0.7f);
        Instantiate(_bullet, transform.position, transform.rotation);

        _animator.Play("Fade2 Boss");

        yield return new WaitForSeconds(_timeBetweenTps);
        _wayPointToGo = Random.Range(0, _wayPoints.Length);

        transform.position = _wayPoints[_wayPointToGo].position;

        yield return new WaitForSeconds(_timeBetweenTps / 1.5f);

        _animator.Play("Fade1 Boss");

        _canTp = true;
    }
}
