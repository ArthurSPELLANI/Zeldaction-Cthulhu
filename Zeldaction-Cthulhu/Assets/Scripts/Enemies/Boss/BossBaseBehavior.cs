using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace Boss
{
    public class BossBaseBehavior : MonoBehaviour
    {
        public int phase1Hp;
        public int phase2Hp;
        [SerializeField] private int currentHp;

        [HideInInspector] public bool isInPhase1 = true;
        public bool isWeak = false;

        Material defaultMaterial;

        public GameObject phase1Go;
        public GameObject phase2Go;
        public GameObject transition;
        public GameObject death;

        void Awake()
        {
            if (isInPhase1)
            {
                currentHp = phase1Hp;
            }
            else
            {
                currentHp = phase2Hp;
            }
            
        }

        void Start()
        {
            defaultMaterial = Resources.Load<Material>("Material/Sprite-Lit-Default");
        }

        void Update()
        {
            if (currentHp <= 0)
            {
                if (isInPhase1 == true)
                {
                    phase1Go.GetComponentInChildren<Phase1PatternManager>().Phase1Over();
                }
                else
                {
                    phase2Go.SetActive(false);
                    phase1Go.SetActive(false);
                    death.SetActive(true);
                }
            }

            if (isWeak == true)
            {
                if (isInPhase1 == true)
                {
                    PlayerManager.Instance.playerLook.enabled = false;
                    PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.position = new Vector2(phase1Go.transform.localPosition.x, phase1Go.transform.localPosition.y);                    
                }
                else
                {
                    PlayerManager.Instance.playerLook.enabled = false;
                    PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.position = new Vector2(phase2Go.transform.localPosition.x, phase2Go.transform.localPosition.y + 1.5f);                    
                }
            }
            else if (PlayerManager.Instance.playerLook.isActiveAndEnabled == false)
            {
                PlayerManager.Instance.playerLook.enabled = true;
            }

            //Feedback quand le joueur peut infliger des dégâts au boss.
            /*if (isWeak == true)
            {
                
                if (isInPhase1 == true)
                {
                    transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                
            }
            else
            {
                if (isInPhase1 == true)
                {
                    transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                
            }*/
        }

        public void BossTakeDamage()
        {
            if(isWeak == true)
            {
                Debug.Log("Mhamhy a prit 1 point de dgt");
                currentHp -= 1;
                StartCoroutine(HitFeedbackBoss());
                isWeak = false;
                GetComponentInChildren<Animator>().SetBool("isWeak", false);

                if (!isInPhase1)
                {
                    if (phase2Go.GetComponentInChildren<Phase2PatternManager>().patternNbr == 1)
                    {
                        phase2Go.GetComponentInChildren<Phase2PatternManager>().canDoPattern1 = false;
                    }

                    else if (phase2Go.GetComponentInChildren<Phase2PatternManager>().patternNbr == 2)
                    {
                        phase2Go.GetComponentInChildren<Phase2PatternManager>().canDoPattern2 = false;
                    }
                }
            }
        }

        public IEnumerator HitFeedbackBoss()
        {
            GetComponentInChildren<SpriteRenderer>().material = Resources.Load<Material>("Material/White");
            yield return new WaitForSeconds(0.05f);
            GetComponentInChildren<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.05f);
            GetComponentInChildren<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.05f);
            GetComponentInChildren<SpriteRenderer>().material = Resources.Load<Material>("Material/Sprite-Lit-Default");
        }

        public void Phase1Done()
        {
            transition.SetActive(true);            
            phase1Go.SetActive(false);
            isWeak = false;
        }

        public void Phase2Begin()
        {
            transition.SetActive(false);
            isInPhase1 = false;
            currentHp = phase2Hp;
            phase2Go.SetActive(true);
        }


    }
}

