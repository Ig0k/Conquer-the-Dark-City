using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] _virtualCameras;
    [SerializeField] private Transform _mouseTransform;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _virtualCameras[0].SetActive(false);
            _virtualCameras[1].SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _virtualCameras[0].SetActive(true);
            _virtualCameras[1].SetActive(false);
        }
    }
}
