using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        public int playerMaxHealth;
        public int playerCurrentHealth;
        public GameObject player;

        public int healNumber;
        public int maxHealNumber;
        bool canHeal;
        bool isHealing;

        void Awake()
        {
            playerCurrentHealth = playerMaxHealth;
            healNumber = 0;
            maxHealNumber = 10;
        }

        void Start()
        { 
        }

        void Update()
        {
            #region healing
            if (healNumber > 0 && playerCurrentHealth < playerMaxHealth)
            {
                canHeal = true;
            }
            else if (healNumber == 0)
            {
                canHeal = false;
            }
            else if (playerCurrentHealth == playerMaxHealth)
            {
                canHeal = false;
            }

            if (isHealing == true && Input.GetButtonUp("Heal"))
            {
                PlayerManager.Instance.playerMovement.speed = 60;
                isHealing = false;
                Debug.Log("le heal a été malencontreusement interompu...");
                StopAllCoroutines();
            }

            if (Input.GetButtonDown("Heal") && canHeal)
            {
                StartCoroutine(Healing());
                Debug.Log("J'utilise une caisse de soin");
            }
            #endregion
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

        IEnumerator Healing()
        {
            isHealing = true;

            PlayerManager.Instance.playerMovement.speed = 30;

            yield return new WaitForSeconds(1f);

            isHealing = false;
            if (playerCurrentHealth == (playerMaxHealth - 1))
            {
                playerCurrentHealth += 1;
            }
            else if (playerCurrentHealth < playerMaxHealth)
            {
                playerCurrentHealth += 2;
            }

            PlayerManager.Instance.playerMovement.speed = 60;
        }


    }
}
