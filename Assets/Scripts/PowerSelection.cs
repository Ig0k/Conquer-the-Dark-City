using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSelection : MonoBehaviour
{
    [SerializeField] PowerManagement _powerManagement;

    private void Awake()
    {
        if(_powerManagement == null) _powerManagement = FindObjectOfType<PowerManagement>();
    }

    public void ActiveFreezePower()
    {
        _powerManagement.ActiveTimeFreeze(true);
    }
    public void ActiveInvisibility()
    {
        _powerManagement.ActiveInvisibility(true);
    }
}
