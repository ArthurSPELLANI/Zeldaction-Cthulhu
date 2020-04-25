using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using PillarSystem;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public Rigidbody2D rb;
        public int damage;
        

    	void Awake()
	    {
	    
	    }
    
        void Start()
        {
            rb.velocity = PlayerManager.Instance.playerShoot.shootDirection * PlayerManager.Instance.playerShoot.bulletSpeed;
            StartCoroutine(DeathByTime());

        }
    
        void Update()
        {
            
        }        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyBasicBehavior>().TakeDamage(damage, transform.position, 100);
            }

            if (collision.gameObject.tag == "Enviro")
            {
                Destroy(this);
            }

            if (collision.gameObject.tag == "pillar")
            {
                collision.gameObject.GetComponent<Pillar>().CorruptionBeam(rb.velocity);
                Destroy(gameObject);
            }
        }

        IEnumerator DeathByTime()
        {
            yield return new WaitForSeconds(5);

            Destroy(gameObject);
        }

    }
}