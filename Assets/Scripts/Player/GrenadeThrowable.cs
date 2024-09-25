using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowable : MonoBehaviour
{

    public float tiempo = 4f;
    [SerializeField] public float explosionTime;

    [SerializeField] private float explosionratio;

    [SerializeField] GameObject prefabExplo;

    // Start is called before the first frame update
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private bool _isPlayer = false;



    private void Start()
    {
        explosionTime += Time.time+ tiempo;
    }








    public void SetProperties(float speed, float destroyTime, int damage)
    {
        _speed = speed;
        _destroyTime = destroyTime;
        _damage = damage;
    }

    private void Update()
    {
        

        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 0.5f, _layerMask);

     

        
            if (hit.collider != null && hit.collider.TryGetComponent<PlayerLife>(out PlayerLife playerLife))
            {
                //if(playerLife.parpadeo) playerLife.Life -= _damage;

                playerLife.Life -= _damage;
                playerLife.ShowParticles();

                if (playerLife.parpadeo)
                {
                    playerLife.StartCoroutine("Parpadeo");
                    Debug.Log("1");
                }

                Destroy(gameObject);
            }
        



        if (Time.time > explosionTime)  { Explosion(); }



        void Explosion()
        {



            Destroy(gameObject);

            Instantiate(prefabExplo, transform.position, transform.rotation);


            Collider2D[] allEnemies = Physics2D.OverlapCircleAll(transform.position, explosionratio);

            foreach (Collider2D item in allEnemies)
            {
                if (item.gameObject.name.Contains("Enemy Prototype") || item.gameObject.name.Contains("Enemy Prototype (1)")) 
                {

                   Destroy(item.gameObject);
                }
            }

        }


    }

}
