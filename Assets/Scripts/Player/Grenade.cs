using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private int grenadeAmount = 1;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))

        {


                collision.GetComponent<PlayerGrenade>().AddGrenades(grenadeAmount);
               
                Destroy(gameObject);
                Debug.Log($"conseguiste {grenadeAmount} granada");
            

        }
        
    }

}
