using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManagement : MonoBehaviour
{
    public static PowerManagement Instance; 

    public static bool canUseTimeFreeze;
    public static bool canUseInvisibility;

    public static bool canUseShootBoost;
    public static bool canUseTripleShoot;

    public static bool canUseMasiveShoot;
    public static bool canUseShield;

    [SerializeField] private bool _canBuyMasiveShoot = true;

    [SerializeField] private bool _isTimeFreezeVisualization;
    [SerializeField] private bool _isInvisibilityVisualization;
    [SerializeField] private bool _canUseShootBoostVisualization;
    [SerializeField] private bool _canUseMasiveShootVisualization;

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
        _canBuyMasiveShoot = true;

        canUseTimeFreeze = true;
        canUseInvisibility = false;

        canUseShootBoost = false;
        canUseTripleShoot = false;

        _isInvisibilityVisualization = canUseInvisibility;
        _isTimeFreezeVisualization = canUseTimeFreeze;

        _canUseShootBoostVisualization = canUseShootBoost;
    }

    public void ActiveMasiveShoot()
    {
        canUseMasiveShoot = true;
    }

    public void ActiveShield()
    {
        canUseShield = true;
    }

    private void Update()
    {
        if (canUseMasiveShoot) _canUseMasiveShootVisualization = true;
        else _canUseMasiveShootVisualization = false;
    }

    public void ActiveShootBoost(bool _canUseShootBoost)
    {
        canUseShootBoost = true;

        _canUseShootBoostVisualization = canUseShootBoost;
    }

    public void ActiveTripleShoot(bool _canUseTripleShoot)
    {
        canUseTripleShoot = true;

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
