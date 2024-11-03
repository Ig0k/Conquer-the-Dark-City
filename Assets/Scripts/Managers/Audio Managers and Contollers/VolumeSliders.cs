using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    [Header("Values")]
    [Range(0f, 1f)][SerializeField] private float _initalMasterVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float _initalMusicVolume = 0.2f;
    [Range(0f, 1f)][SerializeField] private float _initalSFXVolume = 0.35f;

    [Header("UI")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        SetMasterVolume(_initalMasterVolume);
        _masterSlider.value = _initalMasterVolume;

        SetMusicVolume(_initalMusicVolume);
        _musicSlider.value = _initalMusicVolume;

        SetSFXVolume(_initalSFXVolume);
        _sfxSlider.value = _initalSFXVolume;
    }

    public void SetMasterVolume(float value)
    {
        MusicManager.Instance.SetMasterVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        MusicManager.Instance.SetSFXVolume(value);
    }
}
