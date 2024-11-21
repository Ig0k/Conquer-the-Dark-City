using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDivisible : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _damage = 4;

    [SerializeField] private float _minDistanceToAttack = 10f, _minDistanceForWps = 5, _timeBetweenWps = 2f;

    [SerializeField] private Transform[] _wayPoints;

    [SerializeField] private int i = 0;

    [SerializeField] private GameObject[] _childs1, _childs2;
    [SerializeField] private bool _isChild1 = false, _isChild2; //el child 2 no activa nada


    private void Update()
    {
        Patroll();
    }

    private void Patroll()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                    _wayPoints[i].position, _moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _wayPoints[i].position) <= _minDistanceForWps)
        {
            if (i < _wayPoints.Length - 1)
            {
              
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    public void Divide()
    {
        Debug.Log("Divide");
        if(!_isChild1 && !_isChild2)
        {
            //Instantiate(_childs1[0], transform.position, transform.rotation);
            //Instantiate(_childs1[1], transform.position, transform.rotation);
            _childs1[0].SetActive(true);
            _childs1[0].transform.position = transform.position;
            _childs1[1].SetActive(true);
            _childs1[1].transform.position = transform.position;

            gameObject.SetActive(false);
        }
        else if (_isChild1 && !_isChild2) 
        {
            _childs2[0].SetActive(true);
            _childs2[0].transform.position = transform.position;
            _childs2[1].SetActive(true);
            _childs2[1].transform.position = transform.position;
            _childs2[2].SetActive(true);
            _childs2[2].transform.position = transform.position;

            //Instantiate(_childs2[0], transform.position, transform.rotation);
            //Instantiate(_childs2[1], transform.position, transform.rotation);
            //Instantiate(_childs2[2], transform.position, transform.rotation);
            //Instantiate(_childs2[3], transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else if(_isChild2) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.GetComponent<PlayerLife>();

            if (playerLife != null)
            {
                playerLife.Life -= _damage;
                playerLife.ShakeCall();
            }
        }
    }

}
