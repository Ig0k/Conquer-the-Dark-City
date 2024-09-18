using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    private void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
    }

    public void PlayWalk(float suma)
    {
        _animator.SetFloat("movimiento", MathF.Abs(suma));
    }

    public void StopWalk()
    {
        _animator.SetFloat("movimiento", 0);
    }
}
