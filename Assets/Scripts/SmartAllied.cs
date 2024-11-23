using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartAllied : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform[] _wps;
    [SerializeField] private float _minDistanceFromWp = 2f;

    //[SerializeField] private int _life = 2;

    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _rangeToShoot = 15f, _shootCD = 3f;
    private bool _canShoot = true;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _sight;

    [SerializeField] private int i = 0;

    [Header("Sounds")]
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private SoundsManager _audioManager;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        //if (CharacterData.zone1CH1 || CharacterData.zone1CH2
        //    || CharacterData.zone2CH1 || CharacterData.zone2CH2) gameObject.SetActive(true);
        //else gameObject.SetActive(false);     
    }

    private void Update()
    {      
        if (i >= _wps.Length)
        {
            i = _wps.Length - 1;
        }
        if(i < 0)
        {
            i = 0;
        }

        float playerWpsDistance = Vector2.Distance(_wps[i].position, _playerTransform.position);

        if (playerWpsDistance <= _minDistanceFromWp)
        {
            if (i < _wps.Length - 1)
            {
                i++;            
            }
        }
        if(i >= 0) _agent.SetDestination(_wps[i - 1].position);


        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] != null)
            {
                Vector2 dirToEnemies = (_enemies[i].transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToEnemies);
                transform.rotation = lookRotation;

                float distance = Vector2.Distance(_enemies[i].transform.position, transform.position);
                if (distance <= _rangeToShoot && _canShoot)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;

        yield return new WaitForSeconds(_shootCD);
        Instantiate(_bullet, _sight.position, _sight.rotation);
        _audioManager.PlaySound(_shootClip, 0.5f);

        _canShoot = true;
    }
}
