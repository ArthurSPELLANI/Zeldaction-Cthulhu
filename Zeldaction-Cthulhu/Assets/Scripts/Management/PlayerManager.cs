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
        public NearEnemyDetection nearEnemyDetection;
    
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
            nearEnemyDetection = GetComponentInChildren<NearEnemyDetection>();
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

        //fonction à lancer pour load le player avec ses stats sauvegardées
        public void LoadPlayer()
        {
            playerStats.playerCurrentHealth = PlayerPrefs.GetInt("life");
            playerStats.healNumber = PlayerPrefs.GetInt("healNumber");
            playerShoot.ammunitions = PlayerPrefs.GetInt("bulletNumber");
            

            if (PlayerPrefs.GetInt("canUseShadow") == 1)
            {
                playerShadowMode.enabled = true;
                playerShadowMode.maxSanity = PlayerPrefs.GetFloat("maxSanity");
                playerShadowMode.maxActionPoints = PlayerPrefs.GetInt("mawActionPoints");
                playerShadowMode.fragment = PlayerPrefs.GetInt("fragemntNumbre");
            }

            playerShadowMode.sanity = playerShadowMode.maxSanity;
            playerShadowMode.actionPoints = playerShadowMode.maxActionPoints;

            if (PlayerPrefs.GetInt("canUseMachette") == 1)
                playerAttack.enabled = true;
            if (PlayerPrefs.GetInt("canUseGun") == 1)
                playerShoot.enabled = true;
        }
    }
}