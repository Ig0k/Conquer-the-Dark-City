using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int dmg = 5;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();

            if (playerLife != null)
            {
                playerLife.TakeDamage(dmg);
            }

        }
        else if (collision.TryGetComponent<EnemyPrototype>(out EnemyPrototype enemy1))
        {
            enemy1.TakeDamage(dmg);
           
        }

        else if (collision.TryGetComponent<Enemy2>(out Enemy2 enemy2))
        {
            enemy2.TakeDamage(dmg);
            

        }
        else if (collision.TryGetComponent<Kamikaze>(out Kamikaze Kami))
        {
            Kami.TakeDamage(dmg);
            
        }
        else if (collision.TryGetComponent<Enemy3>(out Enemy3 enemy3))
        {
            enemy3.TakeDamage(dmg);
           
        }
        else if (collision.TryGetComponent<Enemy4>(out Enemy4 enemy4))
        {
            enemy4.TakeDamage(dmg);
           
        }
    }



}

