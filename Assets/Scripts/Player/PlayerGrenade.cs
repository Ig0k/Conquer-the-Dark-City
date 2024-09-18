using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{

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


    
    private void Update()
    {
        RemoveGrenades();
    }
}
