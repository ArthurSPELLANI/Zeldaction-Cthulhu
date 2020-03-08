using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShadowMode : MonoBehaviour
    {

        [Range(0, 100)]
        public int shadowSpeed;

        Rigidbody2D shadowRb;

        bool isAxisInUse = false;
        float shadowInput;
        public bool isShadowActivated = false;
        public GameObject shadowObject;
        GameObject player;
                
        float vertical;
        float horizontal;
        Vector2 direction;

        void Awake()
	    {
	    
	    }
    
        void Start()
        {
            
        }
    
        void Update()
        {
            shadowInput = Input.GetAxisRaw("Shadow");

            //Store des valeur d'input du joystick gauche
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            if (shadowInput != 0 && isAxisInUse == false)
            {
                isAxisInUse = true;

                if (isShadowActivated == false)
                {
                    ShadowActivation();                    
                }

                else if (isShadowActivated == true)
                {
                    ShadowExit();
                }               
            }

            else if(shadowInput == 0 && isAxisInUse == true)
            {
                isAxisInUse = false;
            }            


            if(isShadowActivated == true)
            {
                ShadowMove();
            }

            if(isShadowActivated == true && Input.GetButtonDown("Recall"))
            {
                RecallPlayer();
            }

        }
    
        private void ShadowActivation()
        {
            isShadowActivated = true;
            PlayerManager.Instance.playerMovement.playerRb.velocity = new Vector2(0,0);
            player = GameObject.FindGameObjectWithTag("Player");
            shadowObject.SetActive(true);
            shadowObject.transform.position = player.transform.position;
            shadowRb = GetComponentInChildren<Rigidbody2D>();
        }

        private void ShadowExit()
        {
            isShadowActivated = false;
            shadowObject.SetActive(false);
        }

        private void ShadowMove()
        {
            if (vertical >= 0.01)
            {
                vertical = 1;
            }
            else if (vertical <= -0.01)
            {
                vertical = -1;
            }
            else
            {
                vertical = 0;
            }

            if (horizontal >= 0.01)
            {
                horizontal = 1;
            }
            else if (horizontal <= -0.01)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 0;
            }

            direction = new Vector2(horizontal, vertical).normalized;
            shadowRb.velocity = direction * shadowSpeed * Time.fixedDeltaTime;
        }

        private void RecallPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = shadowObject.transform.position;
            isShadowActivated = false;
            shadowObject.SetActive(false);
        }
    
    }
}