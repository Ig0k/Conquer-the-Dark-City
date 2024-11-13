using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float tiempoEfecto = 0.1f;
    [SerializeField] private GameObject effectBullet;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;
    [SerializeField] private PlayerUpgrades _upgrades;

    [SerializeField] private int _damage = 1, _damage2 = 2;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _speed = 40f, _speed2 = 15f;

    [SerializeField] private Transform _mousePoint, _player;

    [SerializeField] private float _shootCD = .7f, _shootCD2 = 1.3f;
    private bool _canShoot = true;

    [Header("Boost Values")]

    [SerializeField] private bool _canUseBoost = false;
    [SerializeField] private bool _boost = false;
    [SerializeField] private float _boostSpeed = 60f, _boostCD = 0.3f;

    [SerializeField] private float _boostDuration = 5f;
    [SerializeField] private float _timeBetweenBost = 8f, _currentCD = 0f;
    private bool _onCD = false;

    [Header("Sounds")]

    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private SoundsManager _audioManager;

    [SerializeField] private GameObject _boostText;

    public float ShootCD
    {
        set { _shootCD = value; }
    }

    private void Awake()
    {
        if (PowerManagement.canUseShootBoost) _canUseBoost = true;
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    private void Start()
    {
        CheckForProperties();
    }

    private void CheckForProperties()
    {
        if (GameManager.currentCharacter == 1 && _upgrades.level == 1)
        {
            //_speed = _upgrades._bulletSpeedCh1Lvl1;
            //_damage = _upgrades._bulletDamageCh1Lvl1;

            _bulletScript.SetProperties(50f, _destroyTime, 2);
            ShootCD = _shootCD;
        }
        else if (GameManager.currentCharacter == 2 && _upgrades.level == 1)
        {
            //_speed = _upgrades._bulletSpeedCh2Lvl1;
            //_damage = _upgrades._bulletDamageCh2Lvl1;

            _bulletScript.SetProperties(35, _destroyTime, 3);
            ShootCD = _shootCD;
        }

        else if (GameManager.currentCharacter == 1 && _upgrades.level == 0)
        {
            _bulletScript.SetProperties(40, _destroyTime, 1);
            ShootCD = _shootCD2;
        }
        else if (GameManager.currentCharacter == 2 && _upgrades.level == 0)
        {
            _bulletScript.SetProperties(27, _destroyTime, 2);
            ShootCD = _shootCD2;
        }
    }

    private IEnumerator BoostTimer()
    {
        _boost = true;
        yield return new WaitForSeconds(_boostDuration);
        _boost = false;
        _currentCD = 0f;
    }

    private void BetweenBoostTimer()
    {
        if (_onCD)
        {
            _currentCD += Time.deltaTime;

            if (_currentCD > _timeBetweenBost)
            {
                _onCD = false;
                _currentCD = 0f;
            }
        }
    }

    private void Update()
    {
        if (_canUseBoost && !_onCD && Input.GetKeyDown(KeyCode.Alpha1) && !_boost)
        {
            StartCoroutine(BoostTimer());
            _onCD = true;
        }

        BetweenBoostTimer();

        if (!_boost)
        {
            if (Input.GetMouseButton(0) && _canShoot) StartCoroutine(Shoot());
            CheckForProperties();

            _boostText.SetActive(false);
        }
        else
        {
            if (Input.GetMouseButton(0) && _canShoot) StartCoroutine(Shoot());
            _bulletScript.SetProperties(_boostSpeed, _destroyTime, 1);

            _boostText.SetActive(true);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;        

        if(PauseMenu.gamePaused == false)
        {
            transform.up = dir;
            transform.position = _player.position;
        }
         //+ new Vector3(0, 1.5f, 0);
    }

    private IEnumerator Shoot()
    {
        if (!_boost)
        {
            _canShoot = false;
            Instantiate(_bullet, transform.position, transform.rotation);
            
           

            _audioManager.PlaySound(_shootClip, 0.7f);
            GameObject instanciaEfec = Instantiate(effectBullet, transform.position, transform.rotation);
            Destroy(instanciaEfec, tiempoEfecto);

            yield return new WaitForSeconds(_shootCD);
            
            _canShoot = true;
            
        }
        else
        {
            _canShoot = false;
            //_boostText.SetActive(true);

            Instantiate(_bullet, transform.position, transform.rotation);
            _audioManager.PlaySound(_shootClip, 0.7f);
            GameObject instanciaEfec = Instantiate(effectBullet, transform.position, transform.rotation);
            Destroy(instanciaEfec, tiempoEfecto);
            yield return new WaitForSeconds(_boostCD);

            //_boostText.SetActive(false);
            _canShoot = true;
          
        }

        
    }
}
