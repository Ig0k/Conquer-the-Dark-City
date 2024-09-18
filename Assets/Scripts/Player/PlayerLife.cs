using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _life = 10;

    [SerializeField] private ParticleSystem _bloodParticles, _healthParticles;

    [SerializeField] private Animator _animator;
    public bool parpadeo = true;
    [SerializeField] private PlayerAnimations _playerAnimations;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerMovement _movement;

    private void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        if(_sprite == null) _sprite = GetComponent<SpriteRenderer>();
        if(_movement == null) _movement = GetComponent<PlayerMovement>();
        if(_playerAnimations == null) _playerAnimations = GetComponentInChildren<PlayerAnimations>();
    }

    public int Life
    {
        get
        {
            return _life; 
        }                   
        
        set
        {
            _life = Mathf.Clamp(_life, 0, 30);

            _life = value;
        }
    }

    public void ShowParticles()
    {
        Instantiate(_bloodParticles, transform.position, Quaternion.identity);
        _bloodParticles.Play();
    }

    public IEnumerator Parpadeo()
    {
        parpadeo = false;
        //_animator.SetBool("Parpadeo", true);
        yield return new WaitForSeconds(.5f);
        //_animator.SetBool("Parpadeo", false);
        parpadeo = true;
    }

    public void ShowHealthParticles()
    {
        Instantiate(_healthParticles, transform.position, Quaternion.identity);
        _healthParticles.Play();
    }

    private void Update()
    {
        if(Life <= 0)
        {
            _sprite.enabled = false;
            _movement.WalkSpeed = 0;
        }
        
    }
}
