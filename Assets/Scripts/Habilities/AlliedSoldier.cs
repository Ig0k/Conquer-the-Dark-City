using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedSoldier : MonoBehaviour
{
    //[SerializeField] private Transform[] _wps;

    [SerializeField] private int _life = 2;

    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _rangeToShoot = 15f, _shootCD = 3f;
    private bool _canShoot = true;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _sight;

    int i = 0;

    [Header("Sounds")]
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private SoundsManager _audioManager;

    private void Awake()
    {
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    private void Start()
    {
        if(CharacterData.zone1CH1) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    private void Update()
    {
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
