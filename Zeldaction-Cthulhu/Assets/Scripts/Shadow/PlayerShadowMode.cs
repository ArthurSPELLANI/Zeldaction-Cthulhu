using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shadow;
using UI;
using Cinemachine;
using AudioManaging;
using Enemy;

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
        public CinemachineVirtualCamera VCamMain;
        public CinemachineVirtualCamera VCamShadow;
        public bool isOutOfBounds = false;

        float timeRef;

        public bool isCharged;

        public float maxSanity;
        public float sanity;
        public GameObject sanityGauge;
        public int sanityDecay;
        public int sanityGain;

        public int actionPoints;
        public int maxActionPoints;
        float cooldownAP = 5f;
        float timestamp;

        public int fragment;

        void Awake()
	    {
            this.enabled = false;     
        }
    
        void OnEnable()
        {
            timeRef = Time.fixedDeltaTime;
            shadowInput = Input.GetAxisRaw("Shadow");
            player = GameObject.Find("Player");
        }

        private void FixedUpdate()
        {
            
        }

        void Update()
        {
            shadowInput = Input.GetAxisRaw("Shadow");

            if (isShadowActivated == true)
            {
                sanity -= Time.fixedDeltaTime * sanityDecay;
                sanityGauge.GetComponent<UISanityGauge>().SetSanity(sanity);
            }
            else if (isShadowActivated == false && sanity < maxSanity)
            {
                sanity += Time.deltaTime * sanityGain;
                sanityGauge.GetComponent<UISanityGauge>().SetSanity(sanity);
            }
            
            if(sanity <= 0)
            {
                ShadowExit();
            }

            if(isOutOfBounds == true)
            {
                sanity -= Time.fixedDeltaTime * sanityDecay * 3;
            }



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

            if (actionPoints < maxActionPoints && timestamp <= Time.time)
            {
                actionPoints += 1;
                timestamp = Time.time + cooldownAP;
            }

            if (isCharged == true)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }


        }
    
        private void ShadowActivation()
        {
            if (isShadowActivated == false)
            {
                SlowItDown();
            }

            isShadowActivated = true;
            VCamMain.gameObject.SetActive(false);
            VCamShadow.gameObject.SetActive(true);
            PlayerManager.Instance.playerMovement.playerRb.velocity = new Vector2(0,0);
            shadowObject.SetActive(true);
            shadowObject.transform.position = player.transform.position;
            shadowRb = GetComponentInChildren<Rigidbody2D>();
            /*FindObjectOfType<AudioManager>().Play("entreeShadow");
            FindObjectOfType<AudioManager>().Play("idleShadow");*/
            AudioManager.Instance.Play("entreeShadow");
            AudioManager.Instance.Play("idleShadow");
        }

        public void ShadowExit()
        {
            shadowObject.transform.position = player.transform.position;
            isShadowActivated = false;
            VCamMain.gameObject.SetActive(true);
            VCamShadow.gameObject.SetActive(false);
            isOutOfBounds = false;
            VCamMain.m_Lens.OrthographicSize = 1.77f;
            shadowObject.GetComponent<PlayerShadowMovement>().shadowSpeed = 55;
            shadowObject.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = timeRef;
            /*FindObjectOfType<AudioManager>().Play("sortieShadow");
            FindObjectOfType<AudioManager>().Stop("idleShadow");*/
            AudioManager.Instance.Play("sortieShadow");
            AudioManager.Instance.Stop("idleShadow");
        }
        

        private void RecallPlayer()
        {            
            player.transform.position = shadowObject.transform.position;
            isShadowActivated = false;
            isOutOfBounds = false;
            VCamMain.gameObject.SetActive(true);
            VCamShadow.gameObject.SetActive(false);
            VCamMain.m_Lens.OrthographicSize = 1.77f;
            shadowObject.GetComponent<PlayerShadowMovement>().shadowSpeed = 55;
            shadowObject.SetActive(false);
            Time.timeScale = 1;
            Time.fixedDeltaTime = timeRef;
        }

        public void SlowItDown()
        {
            Time.timeScale = slowTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Shadow" && isShadowActivated == true)
            {
                isOutOfBounds = true;
                shadowObject.GetComponent<PlayerShadowMovement>().shadowSpeed = 15;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Shadow" && isShadowActivated == true)
            {
                isOutOfBounds = false;
                shadowObject.GetComponent<PlayerShadowMovement>().shadowSpeed = 55;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (isShadowActivated == true)
            {
                if(collision.gameObject.tag == "Enemy")
                {
                    if(collision.gameObject.GetComponent<EnemyBasicBehavior>().catchAnimator.enabled == false)
                    {
                        collision.gameObject.GetComponent<EnemyBasicBehavior>().catchAnimator.enabled = true;
                    }
                }
            }

            if (isShadowActivated == false)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    if (collision.gameObject.GetComponent<EnemyBasicBehavior>().catchAnimator.enabled == true)
                    {
                        collision.gameObject.GetComponent<EnemyBasicBehavior>().CatchOut();
                    }
                }
            }

        }

        public void ShadowEnhance()
        {
            fragment -= 3;
            maxSanity += 15;
            //sanity = maxSanity;
            maxActionPoints += 1;
            //actionPoints = maxActionPoints;
            sanityGauge.GetComponent<UISanityGauge>().SetSanity(sanity);
        }



    }
}