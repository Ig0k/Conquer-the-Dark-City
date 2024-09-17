using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;
    [SerializeField] private PlayerUpgrades _upgrades;

    [SerializeField] private int _damage = 1, _damage2 = 2;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _speed = 40f, _speed2 = 15f;

    [SerializeField] private Transform _mousePoint, _player;

    [SerializeField] private float _shootCD = .7f, _shootCD2 = 1.3f;
    private bool _canShoot = true;

    public float ShootCD
    {
        set { _shootCD = value; }
    }

    private void Start()
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

        else if(GameManager.currentCharacter == 1 && _upgrades.level == 0)
        {
            _bulletScript.SetProperties(40, _destroyTime, 1);
            ShootCD = _shootCD2;
        }
        else if ( GameManager.currentCharacter == 2 && _upgrades.level == 0)
        {
            _bulletScript.SetProperties(27, _destroyTime, 2);
            ShootCD = _shootCD2;
        }
    }

    

    private void Update()
    {
        if(Input.GetMouseButton(0) && _canShoot) StartCoroutine(Shoot());

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        transform.up = dir;

        transform.position = _player.position; //+ new Vector3(0, 1.5f, 0);
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        Instantiate(_bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(_shootCD);
        _canShoot = true;
    }
}
