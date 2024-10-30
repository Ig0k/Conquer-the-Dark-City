using UnityEngine;

public class Kamikaze : MonoBehaviour
{

 

    [SerializeField] private float speed = 3f; // Velocidad de movimiento
    [SerializeField] private float detectionRange = 5f; // Rango de detección
    [SerializeField] private float explosionRange = 0.5f; // Rango para explotar
    [SerializeField] private GameObject explosionPrefab; // Prefab de explosión
    [SerializeField] private Transform player; // Transform del jugador
    [SerializeField] private int health = 3;


    public int TakeDamage(int dmg)
    {

        health--;

        if (health <= 0)
        {
            Destroy(gameObject);

        }
        return health;
    }

    private void Start()
    {
        // Buscar al jugador por su tag al iniciar
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("No se encontró un objeto con el tag 'Player'. Asegúrate de que el jugador tenga este tag.");
        }


    }



    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            MoveTowardsPlayer();

            if (Vector2.Distance(transform.position, player.position) <= explosionRange)
            {
                Explode();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Explode()
    {
        Debug.Log("¡Explosión!");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Instanciar la explosión
        Destroy(gameObject); // Destruir el enemigo
    }
}








   