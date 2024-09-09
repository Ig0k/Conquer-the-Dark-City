using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private int _addLife = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<PlayerLife>().Life += _addLife;
            collision.gameObject.GetComponent<PlayerLife>().ShowHealthParticles();
            Destroy(gameObject);
        }
    }
}
