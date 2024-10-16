using UnityEngine;

public class BarrilRojo : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Bullet Player")) // Aseg�rate de que la bala tenga la etiqueta "Bala"
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instanciar la explosi�n
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destruir el barril
        Destroy(gameObject);
    }
}
