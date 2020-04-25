using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Player;

namespace Boss
{
    public class BossBulletBehavior : MonoBehaviour
    {
        private Rigidbody2D bulletRb;
        private Vector2 vectorDir;
        public float bulletSpeed;
        private Transform target;
        private GameObject player;
        public int bulletDmg;

        [Range(0, 5)]
        public float safeTime;
        private bool canDoDmg = false;

        void Awake()
        {
            bulletRb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
            target = player.transform;
        }

        void Start()
        {
            vectorDir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;

            StartCoroutine(TimeUntilDmg());
        }


        void Update()
        {
            bulletRb.velocity = (vectorDir * bulletSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Small delay before the bullet can inflict damage so it doesn't stop on the enemy that fired it.
        /// </summary>
        /// <returns></returns>
        IEnumerator TimeUntilDmg()
        {
            yield return new WaitForSeconds(safeTime);

            canDoDmg = true;
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            if (canDoDmg == true)
            {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<PlayerStats>().PlayerTakeDamage(bulletDmg);
                }

                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyBasicBehavior>().TakeDamage(bulletDmg, transform.position, 100);
                }

                if (other.CompareTag("Boss"))
                {
                    other.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }

                Destroy(gameObject);
            }

        }

    }
}
