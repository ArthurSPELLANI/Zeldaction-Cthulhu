﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        public int playerMaxHealth;
        public int playerCurrentHealth;
        public GameObject player;

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
            PlayerManager.Instance.playerAttack.coolDown = 0; 
            Debug.Log("Points de vie restant au joueur : " + playerCurrentHealth);

            if (playerCurrentHealth <= 0)
            {
                Destroy(player);
            }
        }


    }
}
