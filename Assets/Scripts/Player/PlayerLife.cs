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

    private void Awake()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
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

        _animator.SetBool("Parpadeo", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("Parpadeo", false);

        parpadeo = true;
    }

    public void ShowHealthParticles()
    {
        Instantiate(_healthParticles, transform.position, Quaternion.identity);
        _healthParticles.Play();
    }

    private void Update()
    {
        //if(Life <= 0) Destroy(gameObject, 2f);
        
    }
}
