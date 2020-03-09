using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Enemy;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public Rigidbody2D rb;

        

    	void Awake()
	    {
	    
	    }
    
        void Start()
        {
            rb.velocity = PlayerManager.Instance.playerShoot.shootDirection * PlayerManager.Instance.playerShoot.bulletSpeed;
        }
    
        void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //collision.GetComponent<EnemyBasicBehaviour>.TakeDamage();
            }

            if (collision.gameObject.tag == "Enviro")
            {
                Destroy(this);
            }

        }

    }
}