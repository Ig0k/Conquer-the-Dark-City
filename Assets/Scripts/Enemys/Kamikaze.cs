using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f; // Velocidad de movimiento
    [SerializeField] private float rangoDeteccion = 5f; // Rango de detección
    [SerializeField] private int vida = 3; // Vida del kamikaze
    [SerializeField] private int dañoExplosión = 3; // Daño que inflige al jugador al explotar
    [SerializeField] private GameObject explosiónPrefab; // Prefab de explosión

    private Transform jugador; // Referencia al jugador

    void Update()
    {
        // Verificar si el jugador está dentro del rango de detección
        if (jugador != null && Vector2.Distance(transform.position, jugador.position) <= rangoDeteccion)
        {
            MoverHaciaJugador();

            // Comprobar si está lo suficientemente cerca del jugador para explotar
            if (Vector2.Distance(transform.position, jugador.position) < 0.5f)
            {
                Explotar();
            }
        }
    }

    private void MoverHaciaJugador()
    {
        // Moverse hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugador = collision.transform; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugador = null; 
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            Explotar(); 
        }
    }

    private void Explotar()
    {
        
        Instantiate(explosiónPrefab, transform.position, Quaternion.identity);

        
        if (jugador != null)
        {
            var playerLife = jugador.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(dañoExplosión); 
            }
        }

        
        Destroy(gameObject);
    }
}


