using UnityEngine;



public class Torreta : MonoBehaviour
{
    [SerializeField]
    private int _life = 10;
    public float rango = 5f; 
    public float tiempoEntreDisparos = 1f; 
    public GameObject proyectilPrefab; 
    private float contadorDisparo = 0f;
    private Transform jugador;
    [SerializeField] GameObject explo;

    void Update()
    {
        if (jugador != null)
        {
            
            Vector2 direccion = jugador.position - transform.position;
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

         
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); 

         
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
        if (collision.CompareTag("Player") )
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

    public int TakeDamage(int damage)
    {
        _life -= damage;
        return _life;
    }

    public void Die() 
    { 
        Instantiate(explo, transform.position, Quaternion.identity);    
        
        Destroy(gameObject); }
}


