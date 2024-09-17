using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public int level = 0;

    [SerializeField] private Bullet _bulletScript;
    public float _bulletSpeedCh1Lvl0 = 40f;
    public int _bulletDamageCh1Lvl0 = 1;

    public float _bulletSpeedCh2Lvl0 = 27f;
    public int _bulletDamageCh2Lvl0 = 2;

    public float _bulletSpeedCh1Lvl1 = 50f;
    public int _bulletDamageCh1Lvl1 = 2;

    public float _bulletSpeedCh2Lvl1 = 35f;
    public int _bulletDamageCh2Lvl1 = 3;

    

    private void Awake()
    {
        level = CharacterData.character1Level;

        if (level == 1 && CharacterData._character == 1)
        {
            Char1Lvl1(_bulletSpeedCh1Lvl1, _bulletDamageCh1Lvl1);
        }
        else if(level == 1 && CharacterData._character == 2)
        {
            Char2Lvl1(_bulletSpeedCh2Lvl1, _bulletDamageCh2Lvl1);
        }
        else if(level == 0 && CharacterData._character == 2)
        {
            Level0Ch2(_bulletSpeedCh2Lvl0, _bulletDamageCh2Lvl0);
        }
        else if (level == 0 && CharacterData._character == 1)
        {
            Level0Ch1(_bulletSpeedCh1Lvl0, _bulletDamageCh1Lvl0);
        }
    }

    public void Level0Ch1(float bulletSpeed, int bulletDamage)
    {
        _bulletSpeedCh1Lvl0 = bulletSpeed;
        _bulletDamageCh1Lvl0 = bulletDamage;
    }

    public void Level0Ch2(float bulletSpeed, int bulletDamage)
    {
        _bulletSpeedCh2Lvl0 = bulletSpeed;
        _bulletDamageCh2Lvl0 = bulletDamage;
    }

    public void Char1Lvl1(float bulletSpeed, int bulletDamage)
    {
        _bulletSpeedCh1Lvl1 = bulletSpeed;
        _bulletDamageCh1Lvl1 = bulletDamage;
    }

    public void Char2Lvl1(float bulletSpeed, int bulletDamage)
    {
        _bulletSpeedCh2Lvl1 = bulletSpeed;
        _bulletDamageCh2Lvl1 = bulletDamage;
    }
}
