using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using AudioManaging;
using XInputDotNetPure;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {

        public int playerMaxHealth;
        public int playerCurrentHealth;
        public GameObject player;

        //variables relatives au heal
        public int healNumber;
        public int maxHealNumber;
        public float timeBeforeHeal;
        bool canHeal;
        public bool isHealing;
        bool isDead = false;

        private Material defaultMaterial;

        private bool canTakeDmg = true;
        public float invuCooldown;


        void Awake()
        {
            playerCurrentHealth = playerMaxHealth;
            healNumber = 0;
            maxHealNumber = 10;
            defaultMaterial = transform.parent.parent.GetChild(0).GetComponent<SpriteRenderer>().material;
        }

        void Start()
        { 

        }

        void Update()
        {
            if (playerCurrentHealth <= 0)
            {
                isDead = true;
            }
            else if (playerCurrentHealth >= 0 && isDead == true)
            {
                isDead = false;
            }

            if(isDead == false)
            {
                #region healing
                //conditions pour savoir si le joueur peut se heal
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

                //interuption du heal
                if (isHealing == true && Input.GetButtonUp("Heal"))
                {
                    PlayerManager.Instance.playerMovement.speed = 60;
                    transform.GetChild(0).gameObject.SetActive(false);
                    isHealing = false;
                    Debug.Log("le heal a été malencontreusement interompu...");
                    StopAllCoroutines();
                    AudioManager.Instance.Stop("healing");
                    AudioManager.Instance.Play("priseHeal");
                }

                //début du heal
                if (Input.GetButtonDown("Heal") && canHeal)
                {
                    StartCoroutine(Healing());
                    AudioManager.Instance.Play("healing");
                    Debug.Log("J'utilise une caisse de soin");
                }
                #endregion
            }
        }


        /// <summary>
        /// Reduce player health by enemy damage
        /// </summary>
        /// <param name="enemyDamage"></param>
        public void PlayerTakeDamage(int enemyDamage)
        {
            if (canTakeDmg)
            {
                playerCurrentHealth -= enemyDamage;
                //PlayerManager.Instance.playerAttack.coolDown = 0; (fix un jour peut etre)
                //PlayerManager.Instance.playerAttack.animator.SetBool("IsAttacking", false);

                if (playerCurrentHealth <= 0)
                {
                    /*Destroy(player);
                    LevelManager.Instance.LoadCurrentScene();
                    PlayerManager.Instance.ResetPosition();
                    PlayerManager.Instance.ResetPlayer();*/
                    PlayerManager.Instance.playerMovement.playerRb.velocity = new Vector2(0, 0);
                    AudioManager.Instance.Play("Mort");
                    PlayerManager.Instance.DisableBehaviour();
                    PlayerManager.Instance.playerAnimator.SetBool("isDead", true);
                }
                else
                {
                    StartCoroutine(DamageShake());
                    AudioManager.Instance.Play("Prisededegats");
                    StartCoroutine(CameraManager.Instance.MainCameraShake(0.5f, 2f, 0.2f));
                }

                StartCoroutine(HitFrames());
                StartCoroutine(Invulnerability());
                
            }
            
        }

        IEnumerator Healing()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isHealing = true;

            PlayerManager.Instance.playerMovement.speed = 30;

            yield return new WaitForSeconds(timeBeforeHeal);

            isHealing = false;
            if (playerCurrentHealth == (playerMaxHealth - 1))
            {
                playerCurrentHealth += 1;
            }
            else if (playerCurrentHealth < playerMaxHealth)
            {
                playerCurrentHealth += 2;
            }

            healNumber -= 1;

            AudioManager.Instance.Play("Soulagement");

            PlayerManager.Instance.playerMovement.speed = 60;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        PlayerIndex playerIndex = default;
        IEnumerator DamageShake()
        {
            GamePad.SetVibration(playerIndex, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            GamePad.SetVibration(playerIndex, 0f, 1f);
            yield return new WaitForSeconds(0.1f);
            GamePad.SetVibration(playerIndex, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            GamePad.SetVibration(playerIndex, 0f, 0f);

        }

        IEnumerator HitFrames()
        {
            transform.parent.parent.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/White");
            yield return new WaitForSeconds(0.1f);
            transform.parent.parent.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.1f);
            transform.parent.parent.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.1f);
            transform.parent.parent.GetChild(0).GetComponent<SpriteRenderer>().material = defaultMaterial;
        }


        IEnumerator Invulnerability()
        {
            float timer = 0.0f;

            while (timer < invuCooldown)
            {
                canTakeDmg = false;
                timer += Time.deltaTime;
                yield return null;
            }

            canTakeDmg = true;
        }

    }
}
