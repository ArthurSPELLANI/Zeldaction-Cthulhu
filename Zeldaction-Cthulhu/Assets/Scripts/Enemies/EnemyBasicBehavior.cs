using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class EnemyBasicBehavior : MonoBehaviour
	{
        public int maxHealth;
        private int currentHealth;
        [Range(0, 100)]
        public int speed;
        public Collider2D fieldOfView;

		void Awake()
		{
            currentHealth = maxHealth;
		}

		void Start()
		{
			
		}

		void Update()
		{

		}



        /// <summary>
        /// Reduce enemy health by player attack damage
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }



    }
}