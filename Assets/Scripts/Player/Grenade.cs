using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private int grenadeAmount = 1;

    [SerializeField]
    PlayerGrenade grenade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)

        {
            
            
                
                collision.GetComponent<PlayerGrenade>().AddGrenades(grenadeAmount);
                Destroy(gameObject);
                Debug.Log($"conseguiste {grenadeAmount} granada");
            

        }
    }

}
