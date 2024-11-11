using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    [SerializeField] private Vignette _vignette;
    [SerializeField] private PostProcessVolume _postProcessVolume;

    [SerializeField] private PlayerLife _playerLife;

    [SerializeField] private int _playerLifeAtStart;

    private float startLife;

    private void Awake()
    {
        if(_playerLife == null) _playerLife = FindObjectOfType<PlayerLife>();
        if(_vignette == null) _postProcessVolume = GetComponent<PostProcessVolume>();

        if (_postProcessVolume.profile.TryGetSettings(out Vignette vignette))
        {
            _vignette = vignette;
        }
    }

    private void Start()
    {
        _playerLifeAtStart = _playerLife.Life;
    }

    private void Update()
    {

        if(_playerLife.Life <= 2)
        {
            float bloodIntensity = (_playerLifeAtStart - _playerLife.Life) / (float)_playerLifeAtStart * 0.5f;
            _vignette.intensity.value = bloodIntensity;
        }
        else
        {
            _vignette.intensity.value = 0f;
        }

    }
}
