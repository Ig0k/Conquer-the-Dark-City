using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;

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
        if (GameManager.currentCharacter == 1)
        {
            _bulletScript.SetProperties(_speed, _destroyTime, _damage);
            ShootCD = _shootCD;
        }
        else if (GameManager.currentCharacter == 2)
        {
            _bulletScript.SetProperties(_speed2, _destroyTime, _damage2);
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
