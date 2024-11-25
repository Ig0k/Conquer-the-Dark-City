using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGrenade : MonoBehaviour
{
    [SerializeField] private int _life = 6;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private float _timeBetweenWps = 4f, _timeBetweenGrenades = 5f;
    [SerializeField] private float _minDistanceForWps = 1.5f, _distanceToThrowGrenade = 15f;
    [SerializeField] private int _wpsIndex;

    [SerializeField] private GameObject _grenade;
    [SerializeField] private newGrenadeWeapon _grenadeScript;

    private bool _canChangeWp = true, _canThrowGrenade = true;

    [Header("Sounds and FX")]
    [SerializeField] private ParticleSystem _bloodParticles;
    [SerializeField] private AudioClip _impactClip;
    [SerializeField] private SoundsManager _audioManager;

    public int TakeDamage(int damage)
    {
        _life -= damage;

        _audioManager.PlaySound(_impactClip, 0.35f);

        Instantiate(_bloodParticles, transform.position, transform.rotation);

        StartCoroutine(SpriteDamaged());

        return _life;
    }

    private void Update()
    {
        if (_life <= 0) Destroy(gameObject);

        if (_agent.isOnNavMesh)
        {
            float distance = Vector2.Distance(transform.position, _playerTransform.position);

            if (_canChangeWp) StartCoroutine(GoToWayPoint());

            if (distance <= _distanceToThrowGrenade)
            {
                if(_canThrowGrenade) StartCoroutine(ThrowGrenade());
            }

        }
    }

    private void Start()
    {
        //ESTO SIRVE PARA QUE EL PERSONAJE NO DESAPAREZCA, POR NO ENTRAR EN MAS DETALLES
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Awake()
    {
        if(_agent == null) _agent = GetComponent<NavMeshAgent>();
        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    private IEnumerator ThrowGrenade()
    {
        _canThrowGrenade = false;

        //_grenadeScript.ThrowForEnemy(Vector2.down);
        _grenadeScript._enemyGrenadeDir = transform.position - _playerTransform.position;
        Instantiate(_grenade, transform.position, transform.rotation);

        Debug.Log("Throwing Grenade!");

        yield return new WaitForSeconds(_timeBetweenGrenades);
        _canThrowGrenade = true;
    }

    private IEnumerator GoToWayPoint()
    {
        _canChangeWp = false;

        yield return new WaitForSeconds(_timeBetweenWps);

        if( _wpsIndex < _wayPoints.Length)
        {
            _agent.SetDestination(_wayPoints[_wpsIndex].position);
            _wpsIndex++;
        }
        else
        {
            _wpsIndex = 0;
        }
        
        _canChangeWp = true;
    }

    private IEnumerator SpriteDamaged()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.08f);
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(.08f);
    }
}
