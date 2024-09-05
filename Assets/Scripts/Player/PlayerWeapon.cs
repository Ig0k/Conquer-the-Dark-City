using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;

    [SerializeField] private int _damage;
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _speed;

    [SerializeField] private Transform _mousePoint, _player;


    private void Start()
    {
        _bulletScript.SetProperties(_speed, _destroyTime, _damage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Instantiate(_bullet, transform.position, transform.rotation);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        transform.up = dir;
        transform.position = _player.position + new Vector3(0, 1.5f, 0);

        _mousePoint.position = mousePos;
    }
}
