using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shadow;

namespace Player
{
    public class PlayerShadowMode : MonoBehaviour
    {
        
        
        [Range(0, 1)]
        public float slowTime;

        Rigidbody2D shadowRb;

        bool isAxisInUse = false;
        float shadowInput;
        public bool isShadowActivated = false;
        public GameObject shadowObject;
        GameObject player;
                       
        public Animator animator;

        float timeRef;

        public bool isCharged;

        void Awake()
	    {
            timeRef = Time.fixedDeltaTime;            
        }
    
        void Start()
        {
            shadowInput = Input.GetAxisRaw("Shadow");
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FixedUpdate()
        {
            
        }

        void Update()
        {
            shadowInput = Input.GetAxisRaw("Shadow");

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

            if (isShadowActivated == true && Input.GetButtonDown("Recall"))
            {
                RecallPlayer();
            }

        }
    
        private void ShadowActivation()
        {
            if (isShadowActivated == false)
            {
                SlowItDown();
            }

            isShadowActivated = true;
            PlayerManager.Instance.playerMovement.playerRb.velocity = new Vector2(0,0);
            shadowObject.SetActive(true);
            shadowObject.transform.position = player.transform.position;
            shadowRb = GetComponentInChildren<Rigidbody2D>();                    
        }

        public void ShadowExit()
        {
            shadowObject.transform.position = player.transform.position;
            isShadowActivated = false;
            shadowObject.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = timeRef;
        }
        

        private void RecallPlayer()
        {            
            player.transform.position = shadowObject.transform.position;
            isShadowActivated = false;
            shadowObject.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = timeRef;
        }

        public void SlowItDown()
        {
            Time.timeScale = slowTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    
    }
}