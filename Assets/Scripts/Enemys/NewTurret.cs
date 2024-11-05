using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurret : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _aim;
    [SerializeField] private TurretAim _aimScript;
    [SerializeField] private PlayerLife _playerLifeScript;
    [SerializeField] private ParticleSystem _explosionParticles;

    [Header("Values")]

    [SerializeField] private int _damage = 3;

    [SerializeField] private float _aimSpeed = 5;
    [SerializeField] private float _distanceToShootPlayer = 15;
    private bool _canShootPlayer = true;
    [SerializeField] private float _timeToShoot = 2f;

    [SerializeField] private float _distanceToMakeDamage = 3f;

    private float _lerpDuration = 0.008f;

    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(_playerTransform.position, transform.position);

        if(distanceFromPlayer <= _distanceToShootPlayer)
        {
            AimFollowPlayer();
        }
        else 
        {
            _aim.position = transform.position;
        }

        if (_aimScript.isOnPlayer)
        {
            if (_canShootPlayer) StartCoroutine(ShootPlayer());  
        }
        else if(!_aimScript.isOnPlayer)
        {
            StopCoroutine(ShootPlayer());
        }

    }

    private void AimFollowPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(_playerTransform.position, _aim.position);

        Vector3 lerpToPlayer = Vector3.Slerp(_aim.position, _playerTransform.position, _lerpDuration);
        _aim.position = lerpToPlayer;

        if (_aimScript.isOnPlayer)
        {
            _lerpDuration = 0.004f;
        }
        else
        {
            _lerpDuration = 0.008f;
        }
    }


    private IEnumerator ShootPlayer()
    {
        _canShootPlayer = false;
     
        yield return new WaitForSeconds(5f);

        if (_aimScript.isOnPlayer)
        {
            yield return new WaitForSeconds(3f);

            //Debug.Log("Pum");
            Instantiate(_explosionParticles, _aim.position, Quaternion.identity);

            float distanceFromAim = Vector2.Distance(_playerTransform.position, _aim.position);

            if(distanceFromAim < 2f) _playerLifeScript.Life -= _damage;

            _aim.position = transform.position;
        }
        
        

        _canShootPlayer = true;
    }
}
