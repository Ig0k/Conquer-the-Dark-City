using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    //FUTURO CODIGO DE MECANICA DE INVISIBILIDAD

    private void Start()
    {
        if(PowerManagement.canUseInvisibility) enabled = true;
        else enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Debug.Log("Invisibility!");
    }
}
