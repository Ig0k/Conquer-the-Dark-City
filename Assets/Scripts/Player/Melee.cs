using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour




{



    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dmgGolpe;


    private void Golpe()
    {

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisions in objetos)
        { 
        
            if (colisions.CompareTag("Enemy"))
            {


               
            }
        
        
        
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
