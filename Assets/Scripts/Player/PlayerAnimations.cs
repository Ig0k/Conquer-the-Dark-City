using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public bool parpadeo = true;

    private void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayWalk(float suma)
    {
        _animator.SetFloat("movimiento", MathF.Abs(suma));
    }

    public void StopWalk()
    {
        _animator.SetFloat("movimiento", 0);
    }

    public IEnumerator PlayerParpadeo()
    {
        parpadeo = false;
        //_animator.SetBool("Parpadeo", true);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.06f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.06f);

        //_spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.5f);
        //_animator.SetBool("Parpadeo", false);
        parpadeo = true;
    }
}
