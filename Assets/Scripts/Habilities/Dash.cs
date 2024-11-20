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
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_onCD && !_isDashing)
            {
                Vector2 dirToMouse = (transform.position - _mousePoint.position).normalized;

                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    StartCoroutine(DashCoroutine(dirToMouse, _dashSpeed / 3));
                }
                else
                {
                    StartCoroutine(DashCoroutine(dirToMouse, _dashSpeed));
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
