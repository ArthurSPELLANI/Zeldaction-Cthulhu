using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using PillarSystem;
using Boss;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public Rigidbody2D rb;
        public int damage;
        public float knockback;

        public int maxEnemyHit;
        private int currentEnemyHit;
        

        public float timeBeforeBulletKill;
        

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
            if (currentEnemyHit >= maxEnemyHit)
            {
                Destroy(gameObject);
            }
        }        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyBasicBehavior>().TakeDamage(damage, transform.position, knockback);
                currentEnemyHit++;
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

            if (collision.gameObject.tag == "Boss")
            {
                collision.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                Destroy(gameObject);
            }
        }

        IEnumerator DeathByTime()
        {
            yield return new WaitForSeconds(timeBeforeBulletKill);

            Destroy(gameObject);
        }

    }
}