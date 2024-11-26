using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerSelection : MonoBehaviour
{
    [SerializeField] PowerManagement _powerManagement;

    [SerializeField] UpgradesStore _upgradeStore;

    [SerializeField] private GameObject _unPurchasedText;

    [SerializeField] private TMP_Text _currentPower;

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

        //else
        //{
        //    _powerManagement.ActiveTimeFreeze(true);
        //    StartCoroutine(UnpurchasedSkill());
        //}
    }

    private void Update()
    {
        if (PowerManagement.canUseInvisibility)
        {
            _currentPower.text = "Invisibility activated";
        }
        else if(PowerManagement.canUseTimeFreeze)
        {
            _currentPower.text = "Time Freeze activated";
        }

        //ACTIVACION AUTOMATICA DEL BOOST DE DISPAROS:

        if(UpgradesStore.shootBoostBought)
        {
            _powerManagement.ActiveShootBoost(true);
        }
    }

    private IEnumerator UnpurchasedSkill()
    {
        _unPurchasedText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _unPurchasedText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _unPurchasedText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _unPurchasedText.SetActive(false);
    }
}
