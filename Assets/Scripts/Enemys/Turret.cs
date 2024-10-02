using UnityEngine;

public class Torreta : MonoBehaviour
{
    [SerializeField]
    private int _life = 10;
    public float rango = 5f; // Rango de detección
    public float tiempoEntreDisparos = 1f; // Tiempo entre disparos
    public GameObject proyectilPrefab; // Prefab del proyectil
    private float contadorDisparo = 0f; // Contador para el tiempo entre disparos
    private Transform jugador; // Referencia al jugador

    void Update()
    {
        if (jugador != null)
        {
            // Rota la torreta hacia el jugador
            Vector2 direccion = jugador.position - transform.position;
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Dispara si el jugador está dentro del rango y el tiempo entre disparos ha pasado
            contadorDisparo -= Time.deltaTime;
            if (contadorDisparo <= 0f)
            {
                Disparar();
                contadorDisparo = tiempoEntreDisparos;
            }
        }

        if (_life <= 0)
        {


            Destroy(gameObject);

        }
    }

    void Disparar()
    {
       
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
        Vector2 direccion = (jugador.position - transform.position).normalized; 
        proyectil.transform.up = direccion; 
        proyectil.GetComponent<Rigidbody2D>().velocity = direccion * 10f; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugador = collision.transform;
            jugador.GetComponent<PlayerLife>().Life --;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugador = null;
        }
    }

    public int TakeDamage(int damage)
    {
        _life -= damage;
        return _life;
    }

    public void Die() { Destroy(gameObject); }
}
