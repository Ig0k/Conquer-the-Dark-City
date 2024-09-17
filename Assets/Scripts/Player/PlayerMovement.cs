using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _walkSpeed = 5;

    [SerializeField] private ParticleSystem _Particle;
    public float WalkSpeed
    {
        set { _walkSpeed = value; }
    }
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if(GameManager.currentCharacter == 1)
        {
            _walkSpeed = 6;

            //Distintas stats segun personaje... Desarrollar qué tiene de diferente cada uno (también en otros scripts)
        }
        else if(GameManager.currentCharacter == 2)
        {
            _walkSpeed = 8; //Velocidad original con que entregamos la build es 8.

            //Distintas stats segun personaje... Desarrollar qué tiene de diferente cada uno (también en otros scripts)
        }
    }

    private void FixedUpdate()
    {
        Walk();
        
    }


    private void Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = (transform.up * vertical + transform.right * horizontal).normalized;
        _rb.velocity = move * _walkSpeed;
        _Particle.Play();

    }

}
