using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManagement : MonoBehaviour
{
    public static PowerManagement Instance; 

    public static bool canUseTimeFreeze;
    public static bool canUseInvisibility;

    public static bool canUseShootBoost;

    [SerializeField] private bool _isTimeFreezeVisualization;
    [SerializeField] private bool _isInvisibilityVisualization;
    [SerializeField] private bool _canUseShootBoostVisualization;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canUseTimeFreeze = true;
        canUseInvisibility = false;

        canUseShootBoost = false;

        _isInvisibilityVisualization = canUseInvisibility;
        _isTimeFreezeVisualization = canUseTimeFreeze;

        _canUseShootBoostVisualization = canUseShootBoost;
    }

    public void ActiveShootBoost(bool _canUseShootBoost)
    {
        canUseShootBoost = true;

        _canUseShootBoostVisualization = canUseShootBoost;
    }

    public void ActiveInvisibility(bool _canUseInvisibility)
    {
        canUseTimeFreeze = false;
        canUseInvisibility = true;

        _isInvisibilityVisualization = canUseInvisibility;
        _isTimeFreezeVisualization = canUseTimeFreeze;
    }

    public void ActiveTimeFreeze(bool _canUseTimeFreeze)
    {
        canUseInvisibility = false;
        canUseTimeFreeze = true;

        _isTimeFreezeVisualization = canUseTimeFreeze;
        _isInvisibilityVisualization = canUseInvisibility;
    }

}
