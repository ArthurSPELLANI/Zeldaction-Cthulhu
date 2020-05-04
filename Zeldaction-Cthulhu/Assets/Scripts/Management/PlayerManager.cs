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
        public Animator playerAnimator;  
    
        public Vector3 nativePosition;
        public Transform currentPosition;

        private void Awake()
        {
            MakeSingleton(true);
            playerAttack = GetComponentInChildren<PlayerAttack>();
            playerMovement = GetComponentInChildren<PlayerMovement>();
            playerShadowMode = GetComponentInChildren<PlayerShadowMode>();
            playerStats = GetComponentInChildren<PlayerStats>();
            playerShoot = GetComponentInChildren<PlayerShoot>();
        }

        void Start()
        {          
            nativePosition = new Vector3(0, 0, 0);
            currentPosition = gameObject.transform;
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