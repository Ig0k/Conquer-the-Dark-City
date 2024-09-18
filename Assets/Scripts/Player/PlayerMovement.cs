using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _walkSpeed = 5;

    [SerializeField] private ParticleSystem _Particle;


    [Header("Animacion")]
    private Animator _Animator;

    [SerializeField] private PlayerAnimations _playerAnimations;

    public float WalkSpeed
    {
        set { _walkSpeed = value; }
    }
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_playerAnimations == null) _playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }

    private void Start()
    {
        _Animator = GetComponent<Animator>();


        if(GameManager.currentCharacter == 1)
        {
            _walkSpeed = 6;

            //Distintas stats segun personaje... Desarrollar qu� tiene de diferente cada uno (tambi�n en otros scripts)
        }
        else if(GameManager.currentCharacter == 2)
        {
            _walkSpeed = 8; //Velocidad original con que entregamos la build es 8.

            //Distintas stats segun personaje... Desarrollar qu� tiene de diferente cada uno (tambi�n en otros scripts)
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

        //ANIMATOR MANAGEMENT

        float suma = horizontal + vertical;

        if(horizontal != 0 || vertical != 0)
        {
            _playerAnimations.PlayWalk(suma);
        }
        else
        {
            _playerAnimations.StopWalk();
        }     
    }

}
