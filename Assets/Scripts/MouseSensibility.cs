using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensibility : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _mouseSensibility;
    [SerializeField] private float _baseSensitivity = 1f;

    private void Start()
    {
        _mouseSensibility = _baseSensitivity * _slider.value;

        _slider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float value)
    {
        _mouseSensibility = _baseSensitivity * value;
    }

    public Vector2 ChangeMouseSensibility()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensibility;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensibility;
        return new Vector2(mouseX, mouseY);
    }
}
