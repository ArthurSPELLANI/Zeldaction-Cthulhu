using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class EnemyBasicBehavior : MonoBehaviour
	{
        public int enemyMaxHealth;
        private int enemyCurrentHealth;
        public int enemyDamage;

        [Range(0, 100)]
        public int speed;

		void Awake()
		{
            enemyCurrentHealth = enemyMaxHealth;
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
        /// <param name="playerDamage"></param>
        public void TakeDamage(int playerDamage)
        {
            enemyCurrentHealth -= playerDamage;

            if(enemyCurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }



    }
}