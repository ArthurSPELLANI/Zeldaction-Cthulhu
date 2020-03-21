using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

    public class Pillar : MonoBehaviour
    {
        public bool isCharged;
        public float pillarCooldown = 0.04f;
        bool canInteract;
        PlayerShadowMode playerShadowMode;

        void Start()
        {

            playerShadowMode = GameObject.Find("ShadowMode").GetComponent<PlayerShadowMode>();    

            if (isCharged == true)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            canInteract = true;
        }

        void Update()
        {
            if(playerShadowMode.isShadowActivated && !isCharged)
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Shadow" && isCharged && canInteract && !playerShadowMode.isCharged)
            {
                UnCharge();
            }
            if (col.tag == "Shadow" && !isCharged && canInteract && playerShadowMode.isCharged)
            {
                Charge();
            }
        }

        void UnCharge()
        {
            isCharged = false;
            canInteract = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(ChargeCooldown());
            playerShadowMode.isCharged = true;

        }

        void Charge()
        {
            isCharged = true;
            canInteract = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(ChargeCooldown());
            playerShadowMode.isCharged = false;
        }

        IEnumerator ChargeCooldown()
        {
            yield return new WaitForSeconds(pillarCooldown);
            canInteract = true;
        }
    }
