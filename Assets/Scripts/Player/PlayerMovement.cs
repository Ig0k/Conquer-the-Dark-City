using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _walkSpeed = 5;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Walk();
    }


    private void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _rb.velocity = ((transform.up * vertical + transform.right * horizontal) * _walkSpeed);

    }
}
