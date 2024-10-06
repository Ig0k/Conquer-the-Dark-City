using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money Instance;

    private static int _money = 100;
    [SerializeField] private int _moneyViewer;

    private void Update()
    {
        _moneyViewer = _money;
    }

    public static int money
    {
        get { return _money; } 
        set 
        { 
            _money = Mathf.Clamp(_money, 0, 10000);
            _money = value;
        }
    }

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
}
