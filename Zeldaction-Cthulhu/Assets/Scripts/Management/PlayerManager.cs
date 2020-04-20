using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {

        public PlayerMovement playerMovement;
        public PlayerAttack playerAttack;
        public PlayerShadowMode playerShadowMode;
        public PlayerStats playerStats;
        public PlayerShoot playerShoot;

        public Vector3 nativePosition;

        private void Awake()
        {
            MakeSingleton(true);
        }

        void Start()
        {
            playerAttack = GetComponentInChildren<PlayerAttack>();
            playerMovement = GetComponentInChildren<PlayerMovement>();
            playerShadowMode = GetComponentInChildren<PlayerShadowMode>();
            playerStats = GetComponentInChildren<PlayerStats>();
            playerShoot = GetComponentInChildren<PlayerShoot>();

            nativePosition = new Vector3(0, 0, 0);
        }
    
        void Update()
        {
            
        }

        public void ResetPlayer()
        {
            playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
            playerShadowMode.sanity = playerShadowMode.maxSanity;
            playerShadowMode.actionPoints = 5;
            playerShoot.ammunitions = 6;
        }



    }
}