using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MassiveShoot : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShoots;
    [SerializeField] private bool _canShoot = true;

    [SerializeField] private int _shootsLeft = 2;

    [SerializeField] private Transform[] _bulletSpawnPos;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Bullet _bulletScript;

    private void Awake()
    {
        if(CharacterData._character == 1)
        {
            if (PowerManagement.canUseMasiveShoot) enabled = true;
            else enabled = false;
        }
        else
        {
            enabled = false;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canShoot && _shootsLeft > 0)
        {
            //_bulletScript.SetProperties(5, 6, 3);
            StartCoroutine(Shoot());

            _shootsLeft--;
        }

    }

    private IEnumerator Shoot()
    {
        _canShoot = false;

        _bulletScript.SetProperties(6, 6, 3);

        Instantiate(_bullet, _bulletSpawnPos[0].position, _bulletSpawnPos[0].rotation);
        Instantiate(_bullet, _bulletSpawnPos[1].position, _bulletSpawnPos[1].rotation);
        Instantiate(_bullet, _bulletSpawnPos[2].position, _bulletSpawnPos[2].rotation);
        Instantiate(_bullet, _bulletSpawnPos[3].position, _bulletSpawnPos[3].rotation);
        Instantiate(_bullet, _bulletSpawnPos[4].position, _bulletSpawnPos[4].rotation);
        Instantiate(_bullet, _bulletSpawnPos[5].position, _bulletSpawnPos[5].rotation);
        Instantiate(_bullet, _bulletSpawnPos[6].position, _bulletSpawnPos[6].rotation);
        Instantiate(_bullet, _bulletSpawnPos[7].position, _bulletSpawnPos[7].rotation);
        Instantiate(_bullet, _bulletSpawnPos[8].position, _bulletSpawnPos[8].rotation);
        Instantiate(_bullet, _bulletSpawnPos[9].position, _bulletSpawnPos[9].rotation);
        Instantiate(_bullet, _bulletSpawnPos[10].position, _bulletSpawnPos[10].rotation);
        Instantiate(_bullet, _bulletSpawnPos[11].position, _bulletSpawnPos[11].rotation);
        Instantiate(_bullet, _bulletSpawnPos[12].position, _bulletSpawnPos[12].rotation);
        Instantiate(_bullet, _bulletSpawnPos[13].position, _bulletSpawnPos[13].rotation);
        Instantiate(_bullet, _bulletSpawnPos[14].position, _bulletSpawnPos[14].rotation);
        Instantiate(_bullet, _bulletSpawnPos[15].position, _bulletSpawnPos[15].rotation);

        yield return new WaitForSeconds(_timeBetweenShoots);

        _bulletScript.SetProperties(6, 6, 3);

        Instantiate(_bullet, _bulletSpawnPos[0].position, _bulletSpawnPos[0].rotation);
        Instantiate(_bullet, _bulletSpawnPos[1].position, _bulletSpawnPos[1].rotation);
        Instantiate(_bullet, _bulletSpawnPos[2].position, _bulletSpawnPos[2].rotation);
        Instantiate(_bullet, _bulletSpawnPos[3].position, _bulletSpawnPos[3].rotation);
        Instantiate(_bullet, _bulletSpawnPos[4].position, _bulletSpawnPos[4].rotation);
        Instantiate(_bullet, _bulletSpawnPos[5].position, _bulletSpawnPos[5].rotation);
        Instantiate(_bullet, _bulletSpawnPos[6].position, _bulletSpawnPos[6].rotation);
        Instantiate(_bullet, _bulletSpawnPos[7].position, _bulletSpawnPos[7].rotation);
        Instantiate(_bullet, _bulletSpawnPos[8].position, _bulletSpawnPos[8].rotation);
        Instantiate(_bullet, _bulletSpawnPos[9].position, _bulletSpawnPos[9].rotation);
        Instantiate(_bullet, _bulletSpawnPos[10].position, _bulletSpawnPos[10].rotation);
        Instantiate(_bullet, _bulletSpawnPos[11].position, _bulletSpawnPos[11].rotation);
        Instantiate(_bullet, _bulletSpawnPos[12].position, _bulletSpawnPos[12].rotation);
        Instantiate(_bullet, _bulletSpawnPos[13].position, _bulletSpawnPos[13].rotation);
        Instantiate(_bullet, _bulletSpawnPos[14].position, _bulletSpawnPos[14].rotation);
        Instantiate(_bullet, _bulletSpawnPos[15].position, _bulletSpawnPos[15].rotation);

        yield return new WaitForSeconds(_timeBetweenShoots);

        _bulletScript.SetProperties(6, 6, 3);

        Instantiate(_bullet, _bulletSpawnPos[0].position, _bulletSpawnPos[0].rotation);
        Instantiate(_bullet, _bulletSpawnPos[1].position, _bulletSpawnPos[1].rotation);
        Instantiate(_bullet, _bulletSpawnPos[2].position, _bulletSpawnPos[2].rotation);
        Instantiate(_bullet, _bulletSpawnPos[3].position, _bulletSpawnPos[3].rotation);
        Instantiate(_bullet, _bulletSpawnPos[4].position, _bulletSpawnPos[4].rotation);
        Instantiate(_bullet, _bulletSpawnPos[5].position, _bulletSpawnPos[5].rotation);
        Instantiate(_bullet, _bulletSpawnPos[6].position, _bulletSpawnPos[6].rotation);
        Instantiate(_bullet, _bulletSpawnPos[7].position, _bulletSpawnPos[7].rotation);
        Instantiate(_bullet, _bulletSpawnPos[8].position, _bulletSpawnPos[8].rotation);
        Instantiate(_bullet, _bulletSpawnPos[9].position, _bulletSpawnPos[9].rotation);
        Instantiate(_bullet, _bulletSpawnPos[10].position, _bulletSpawnPos[10].rotation);
        Instantiate(_bullet, _bulletSpawnPos[11].position, _bulletSpawnPos[11].rotation);
        Instantiate(_bullet, _bulletSpawnPos[12].position, _bulletSpawnPos[12].rotation);
        Instantiate(_bullet, _bulletSpawnPos[13].position, _bulletSpawnPos[13].rotation);
        Instantiate(_bullet, _bulletSpawnPos[14].position, _bulletSpawnPos[14].rotation);
        Instantiate(_bullet, _bulletSpawnPos[15].position, _bulletSpawnPos[15].rotation);

        _canShoot = true;
    }

}
