using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagsZone3 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public int currentFlag = 0;

    [SerializeField] private Enemy2[] _enemy2Script;
    [SerializeField] private EnemyPrototype[] _enemy1Script;
    [SerializeField] private NewEnemy1[] _newEnemyScript;
    [SerializeField] private Enemy3[] _enemy3Script;

    public bool flagConquisted = false;
    [SerializeField] private int i1 = 0, i2 = 0, i3 = 0, i4 = 0;

    private void Awake()
    {
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        i1 = 0;

        if (_enemy2Script != null)
        {
            for (int i = 0; i < _enemy2Script.Length; i++)
            {
                if (_enemy2Script[i] == null)
                {
                    i1++;
                }
            }
        }
        else i1++;

        i2 = 0;

        if (_enemy1Script != null)
        {
            for (int i = 0; i < _enemy1Script.Length; i++)
            {
                if (_enemy1Script[i] == null)
                {
                    i2++;
                }
            }
        }
        else i2++;

        i3 = 0;

        if (_enemy3Script != null)
        {
            for (int i = 0; i < _enemy3Script.Length; i++)
            {
                if (_enemy3Script[i] == null)
                {
                    i3++;
                }
            }
        }
        else i3++;

        i4 = 0;
        if (_newEnemyScript != null)
        {
            for (int i = 0; i < _newEnemyScript.Length; i++)
            {
                if (_newEnemyScript[i] == null)
                {
                    i4++;
                }
            }
        }
        else i4++;

        if (i1 >= _enemy2Script.Length && i2 >= _enemy1Script.Length && i3 >= _enemy3Script.Length &&
            i4 >= _newEnemyScript.Length)
        {
            flagConquisted = true;
            _spriteRenderer.color = Color.green;
        }
    }
}
