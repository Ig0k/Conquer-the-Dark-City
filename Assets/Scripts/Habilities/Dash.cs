using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _mousePoint;

    [SerializeField] private float _dashSpeed = 1f;
    [SerializeField] private float _dashDuration = 0.3f;

    [SerializeField] private float _maxCD = 2f;
    [SerializeField] private float _currentCD = 0f;
    [SerializeField] private bool _onCD = false;
    private bool _isDashing = false;

    private void Update()
    {
        if (enabled)
        {
            Vector2 dirToMouse = (_mousePoint.position - transform.position).normalized;
            Vector2 dashDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) // Arriba Izquierda
            {
                dashDirection = new Vector2(-1, 1).normalized;
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) // Arriba Derecha
            {
                dashDirection = new Vector2(1, 1).normalized;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) // Abajo Derecha
            {
                dashDirection = new Vector2(1, -1).normalized;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) // Abajo Izquierda
            {
                dashDirection = new Vector2(-1, -1).normalized;
            }
            else if (Input.GetKey(KeyCode.W)) 
            {
                dashDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S)) 
            {
                dashDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A)) 
            {
                dashDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D)) 
            {
                dashDirection = Vector2.right;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !_onCD && !_isDashing)
            {
                if (dashDirection != Vector2.zero)
                {
                    StartCoroutine(DashCoroutine(dashDirection, _dashSpeed));
                }
                else
                {
                    StartCoroutine(DashCoroutine(Vector2.down, _dashSpeed));
                }
            }

            Timer();
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction, float speed)
    {
        _isDashing = true;
        _onCD = true;

        float elapsed = 0f;

        while (elapsed < _dashDuration)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;
    }

    private void Timer()
    {
        if (_onCD)
        {
            _currentCD += Time.deltaTime;

            if (_currentCD > _maxCD)
            {
                _onCD = false;
                _currentCD = 0f;
            }
        }
    }
}
