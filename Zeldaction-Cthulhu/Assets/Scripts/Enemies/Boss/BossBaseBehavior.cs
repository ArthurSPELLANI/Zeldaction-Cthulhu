using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    public class BossBaseBehavior : MonoBehaviour
    {
        public int phase1Hp;
        public int phase2Hp;
        [SerializeField] private int currentHp;

        [HideInInspector] public bool isInPhase1 = true;
        public bool isWeak = false;

        public GameObject phase1Go;
        public GameObject phase2Go;

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

        }

        void Update()
        {
            if (currentHp == 0)
            {
                if (isInPhase1 == true)
                {
                    isInPhase1 = false;
                    currentHp = phase2Hp;
                    phase1Go.SetActive(false);
                    phase2Go.SetActive(true);
                    isWeak = false;
                }
                else
                {
                    Debug.Log("tu à vaincu la terrible Mhamhy");
                    Destroy(gameObject);
                }
            }

            //Feedback quand le joueur peut infliger des dégâts au boss.
            if (isWeak == true)
            {
                
                if (isInPhase1 == true)
                {
                    transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                }
                else
                {
                    transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                }
                
            }
            else
            {
                if (isInPhase1 == true)
                {
                    transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                }
                
            }
        }

        public void BossTakeDamage()
        {
            if(isWeak == true)
            {
                Debug.Log("Mhamhy a prit 1 point de dgt");
                currentHp -= 1;
                transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.red;
                isWeak = false;

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

    }
}

