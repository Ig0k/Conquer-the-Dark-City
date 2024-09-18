using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] private Transform _mousePoint;

    private void Update()
    {
        Vector2 mousePos = _mousePoint.position;

        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        transform.up = -dir;
    }
}
