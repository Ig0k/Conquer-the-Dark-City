using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerpunch : MonoBehaviour
{
    [SerializeField] private GameObject _punch;
    [SerializeField] private Bullet _bulletScript;

    [SerializeField] private int _damage = 1, _damage2 = 2;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _speed = 40f, _speed2 = 15f;

    [SerializeField] private Transform _mousePoint, _player;

    [SerializeField] private float _PunchCD = .7f, _PunchCD2 = 1.3f;
    private bool _canPunch = true;

    public float PunchCD
    {
        set { _PunchCD = value; }
    }

    private void Start()
    {
        if (GameManager.currentCharacter == 1)
        {
            _bulletScript.SetProperties(_speed, _destroyTime, _damage);
            PunchCD = _PunchCD;
        }
        else if (GameManager.currentCharacter == 2)
        {
            _bulletScript.SetProperties(_speed2, _destroyTime, _damage2);
            PunchCD = _PunchCD2;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && _canPunch) StartCoroutine(Shoot());

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        transform.up = dir;

        transform.position = _player.position;/* + new Vector3(0, 1.5f, 0);*/
    }

    private IEnumerator Shoot()
    {
        _canPunch = false;
        Instantiate(_punch, transform.position, transform.rotation);
        yield return new WaitForSeconds(_PunchCD);
        _canPunch = true;
    }
}
