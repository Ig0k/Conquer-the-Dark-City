using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private int _addLife = 2;

    [Header("Sounds")]
    [SerializeField] private AudioClip _healthClip;
    [SerializeField] private SoundsManager _audioManager;

    private void Awake()
    {
        if(_audioManager == null) _audioManager = FindObjectOfType<SoundsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<PlayerLife>().Life += _addLife;

            _audioManager.PlaySound(_healthClip, 0.8f);
            collision.gameObject.GetComponent<PlayerLife>().ShowHealthParticles();
            Destroy(gameObject);
        }
    }
}
