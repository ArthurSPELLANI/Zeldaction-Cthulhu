using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        public int playerMaxHealth;
        private int playerCurrentHealth;

        void Awake()
        {
            playerCurrentHealth = playerMaxHealth;
        }

        void Start()
        {
 
        }

        void Update()
        {

        }


        /// <summary>
        /// Reduce player health by enemy damage
        /// </summary>
        /// <param name="enemyDamage"></param>
        public void PlayerTakeDamage(int enemyDamage)
        {
            playerCurrentHealth -= enemyDamage;
            Debug.Log("Points de vie restant au joueur : " + playerCurrentHealth);

            if (playerCurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }


    }
}
