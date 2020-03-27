using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    public class BossBaseBehavior : MonoBehaviour
    {
        public int phase1Hp;
        public int phase2Hp;
        private int currentHp;

        private bool isInPhase1 = true;
        public bool isWeak = false;

        public GameObject phase1Go;
        public GameObject phase2Go;

        void Awake()
        {
            currentHp = phase1Hp;
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
                }
                else
                {
                    Debug.Log("tu à vaincu la terrible Mhamhy");
                    Destroy(transform.parent);
                }
            }

            //Feedback quand le joueur peut infliger des dégâts au boss.
            if (isWeak == true)
            {
                transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
            }
        }

        public void BossTakeDamage()
        {
            if(isWeak == true)
            {
                currentHp -= 1;
                transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }

        }

    }
}

