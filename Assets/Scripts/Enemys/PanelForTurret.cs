using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelForTurret : MonoBehaviour
{
    [SerializeField] private GameObject _turret, _aim;

    [SerializeField] private int _life = 2;

    public void TakeDamage(int damage)
    {
        _life -= damage;

        if(_life <= 0 )
        {
            Destroy(_turret);
            Destroy(_aim);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Bullet Player"))
        {
            TakeDamage(1);
        }
    }
}
