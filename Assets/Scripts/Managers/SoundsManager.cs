using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio, float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);

        myAudioSource.PlayOneShot(audio, volume);
    }
}
