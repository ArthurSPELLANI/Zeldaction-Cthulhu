﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public Rigidbody2D rb;
        public int damage;
        Pillar pillar;
        

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
                collision.gameObject.GetComponent<EnemyBasicBehavior>().TakeDamage(damage);
            }

            if (collision.gameObject.tag == "Enviro")
            {
                Destroy(this);
            }

            if (collision.gameObject.tag == "pillar")
            {
                collision.gameObject.GetComponent<Pillar>().CorruptionBeam(rb.velocity);
                Destroy(this);
            }
        }

        IEnumerator DeathByTime()
        {
            yield return new WaitForSeconds(5);

            Destroy(gameObject);
        }

    }
}