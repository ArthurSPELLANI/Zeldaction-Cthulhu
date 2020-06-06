using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using PillarSystem;
using Boss;
using AudioManaging;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public Rigidbody2D rb;
        public int damage;
        public float knockback;

        public int maxEnemyHit;
        private int currentEnemyHit;
        private bool isSafe = false;
        

        public float timeBeforeBulletKill;
        

    	void Awake()
	    {
            StartCoroutine(SafeTime());

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
                AudioManager.Instance.Play("impactEnnemi");
                currentEnemyHit++;
            }

            if (collision.gameObject.tag == "Enviro")
            {
                Destroy(this);
            }

            if (collision.gameObject.tag == "pillar")
            {
                collision.gameObject.GetComponent<Pillar>().CorruptionBeam(rb.velocity);
                AudioManager.Instance.Play("tappagePillier");
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Boss")
            {
                collision.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 8)
            {
                if (isSafe)
                {
                    collision.GetComponent<PlayerStats>().PlayerTakeDamage(2);
                }

            }
        }

        IEnumerator DeathByTime()
        {
            yield return new WaitForSeconds(timeBeforeBulletKill);

            Destroy(gameObject);
        }

        IEnumerator SafeTime()
        {
            yield return new WaitForSeconds(0.5f);

            isSafe = true;
        }

    }
}