using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{

    [SerializeField] private GameObject _grenade;
    [SerializeField] private Bullet _bulletScript;

    [SerializeField] private int _damage = 1, _damage2 = 2;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _speed = 40f, _speed2 = 15f;

    [SerializeField] private Transform _mousePoint, _player;

    [SerializeField] private float _grenadeCD = .12f, _PunchCD2 = 12f;
    private bool _canGrenade = true;


    [SerializeField]
    private int grenadeAvailable;

    public void AddGrenades(int amount)
    {
        grenadeAvailable += amount;
    }

    

    public void RemoveGrenades()
    {
        if (Input.GetKeyDown(KeyCode.G)   &&grenadeAvailable > 0)
        {
            grenadeAvailable -= 1;




            Debug.Log("tire una granada");
        }
        else if (Input.GetKeyDown(KeyCode.G) && grenadeAvailable <= 0)
        {
            Debug.Log("no tengo granadas");
        }
    }



    

    public float GrenadeCD
    {
        set { _grenadeCD = value; }
    }

    private void Start()
    {
        if (GameManager.currentCharacter == 1)
        {
            _bulletScript.SetProperties(_speed, _destroyTime, _damage);
            GrenadeCD = _grenadeCD;
        }
        else if (GameManager.currentCharacter == 2)
        {
            _bulletScript.SetProperties(_speed2, _destroyTime, _damage2);
            GrenadeCD = _PunchCD2;
        }
    }

    private void Update()
    {
        if(grenadeAvailable > 0) { _canGrenade = true; }
        else {  _canGrenade = false; }
        if (Input.GetKeyDown(KeyCode.G) && _canGrenade)

        { GrenadeAction(); }

        

        RemoveGrenades();
    }

    private void GrenadeAction()
    {
        _canGrenade = false;
        Instantiate(_grenade, transform.position, transform.rotation);

        Vector2 dir = (_mousePoint.position - transform.position).normalized;

        _grenade.GetComponent<Rigidbody2D>().AddForce(_speed * dir, ForceMode2D.Impulse);
       
        _canGrenade = true;
    }




}