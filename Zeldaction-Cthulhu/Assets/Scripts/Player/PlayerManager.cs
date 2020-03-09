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
        }
    
        void Update()
        {
            
        }
    
    
    
    }
}