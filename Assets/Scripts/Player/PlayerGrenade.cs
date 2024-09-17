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

        public void RemoveGrenades(int amount)
        {
            if (Input.GetKeyDown(KeyCode.G)   &&grenadeAvailable > 0)
            {
                grenadeAvailable -= amount;

                Debug.Log("tire una granada");
            }
            else
            {
                Debug.Log("no tengo granadas");
            }
        }
}
