using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Game;

namespace Player
{
    public class PlayerManager : MonoBehaviour
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

        public Animator[] trackAnimators = new Animator[2];
        public GameObject[] trackObjects = new GameObject[8];
        public GameObject baseUI;
        public GameObject nearEnemy;

        static public PlayerManager Instance;

        private void Awake()
        {
            //MakeSingleton(true);

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Nique ta mère Jo !");
            }

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

            trackAnimators[0] = GetComponent<Animator>();
            trackAnimators[1] = GameObject.Find("Graphics_Player").GetComponent<Animator>();
            trackObjects[0] = GameObject.Find("Attack");
            trackObjects[1] = GameObject.Find("Movement_Player");
            trackObjects[2] = GameObject.Find("Attack");
            trackObjects[3] = GameObject.Find("ShadowMode");
            trackObjects[4] = GameObject.Find("Coeurs");
            trackObjects[5] = GameObject.Find("Ammunitions");
            trackObjects[6] = GameObject.Find("SanityGauge");
            trackObjects[7] = GameObject.Find("Behaviour_Player");
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPlayer();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                LevelManager.Instance.Town();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                LevelManager.Instance.LightForest1();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                LevelManager.Instance.DarkForest();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                LevelManager.Instance.Cave();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                LevelManager.Instance.CorruptedForest();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                LevelManager.Instance.TownDestroyed();
            }

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