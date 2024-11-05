using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerpunch : MonoBehaviour
{
    [SerializeField] private GameObject _punch;
    [SerializeField] private Bullet _bulletScript;

    [SerializeField] private int _damage = 1, _damage2 = 2;
    [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private float _speed = 40f, _speed2 = 15f;

    [SerializeField] private Transform _mousePoint, _player;

    [SerializeField] private float _PunchCD = .7f, _PunchCD2 = 1.3f;
    private bool _canPunch = true;

    public float PunchCD
    {
        set { _PunchCD = value; }
    }

    private void Start()
    {
        if (GameManager.currentCharacter == 1)
        {
            _bulletScript.SetProperties(_speed, _destroyTime, _damage);
            PunchCD = _PunchCD;
        }
        else if (GameManager.currentCharacter == 2)
        {
            _bulletScript.SetProperties(_speed2, _destroyTime, _damage2);
            PunchCD = _PunchCD2;
        }
    }

    private void Update()
    {
        // Si el jugador presiona el botón de mouse derecho (botón 1)
        if (Input.GetMouseButton(1) && _canPunch) StartCoroutine(Shoot());

        // Obtener la posición del ratón en el mundo
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcular la dirección hacia el ratón y rotar el jugador hacia esa dirección
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        transform.up = dir;

        // Mantener la posición del jugador (aquí es donde debería moverse el jugador en el juego)
        transform.position = _player.position;
    }

    private IEnumerator Shoot()
    {
        _canPunch = false;

        // Calcular la posición donde aparecerá el golpe (frente al jugador)
        Vector3 punchPosition = transform.position + transform.up * 1f; // Aquí 1.5f es la distancia frente al jugador

        // Instanciar el golpe en la posición calculada y con la rotación actual del jugador
        Instantiate(_punch, punchPosition, transform.rotation);

        // Esperar el tiempo de cooldown antes de poder volver a golpear
        yield return new WaitForSeconds(_PunchCD);

        _canPunch = true;
    }
}
