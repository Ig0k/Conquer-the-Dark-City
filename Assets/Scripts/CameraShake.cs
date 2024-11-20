using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera cinemachineVCam;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer = 0f;
    private float shakeIntensity = 0.3f; 

    private void Awake()
    {
        cinemachineVCam = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (PauseMenu.gamePaused == false)
        {
            shakeIntensity = intensity;
            shakeTimer = time;

            //Debug.Log("Cam Shake Base Called");
        }   
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Random.Range(0f, shakeIntensity);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}
