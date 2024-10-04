using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManagement : MonoBehaviour
{
    public static PowerManagement Instance; 

    public static bool canUseTimeFreeze;
    public static bool canUseInvisibility;

    [SerializeField] private bool _isTimeFreezeVisualization;
    [SerializeField] private bool _isInvisibilityVisualization;

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

        _isInvisibilityVisualization = canUseInvisibility;
        _isTimeFreezeVisualization = canUseTimeFreeze;
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
