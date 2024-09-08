using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _life = 10;

    [SerializeField] private ParticleSystem _bloodParticles;

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

    private void Update()
    {
        //if(Life <= 0) Destroy(gameObject, 2f);
        
    }
}
