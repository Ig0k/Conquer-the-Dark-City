using JetBrains.Annotations;
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

    [SerializeField] private CameraShake _camShake;

    [Header("Sounds")]
    [SerializeField] private AudioClip _impactClip;
    [SerializeField] private SoundsManager _audioManager;

    private void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        if(_sprite == null) _sprite = GetComponent<SpriteRenderer>();
        if(_movement == null) _movement = GetComponent<PlayerMovement>();
        if(_playerAnimations == null) _playerAnimations = GetComponentInChildren<PlayerAnimations>();
        if(_camShake == null) _camShake = FindObjectOfType<CameraShake>();
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
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

    public void ShakeCall() //SONIDO Y SHAKE AC�
    {
        Shake(6f, 0.42f);
        _audioManager.PlaySound(_impactClip, 0.6f);
    }

    public void TakeDamage(int dmg )
    {

        _life -= dmg;

    }
    private void Shake(float shakeIntensity, float shakeDuration)
    {
        _camShake.ShakeCamera(shakeIntensity, shakeDuration);
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
