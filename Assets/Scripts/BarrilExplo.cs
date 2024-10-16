using UnityEngine;

public class BarrilRojo : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Bullet Player")) // Asegúrate de que la bala tenga la etiqueta "Bala"
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instanciar la explosión
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destruir el barril
        Destroy(gameObject);
    }
}
