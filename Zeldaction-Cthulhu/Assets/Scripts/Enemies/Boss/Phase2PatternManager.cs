using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Phase2PatternManager : MonoBehaviour
    {
        public int patternNbr;
        private int patternSelector;
        private int patternCount;
        private bool isWaitingForNextPattern = true;

        [HideInInspector] public bool canDoPattern1 = true;
        [HideInInspector] public bool canDoPattern2 = true;

        void Awake()
        {
            patternNbr = 2;
            transform.parent.GetComponentInParent<BossBaseBehavior>().isInPhase1 = false;

        }

        void Start()
        {

        }

        void Update()
        {
            if (patternNbr == 1 && isWaitingForNextPattern == true && !canDoPattern1)
            {
                if (patternCount > 0)
                {
                    GetComponent<Phase2Pattern1>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase2Pattern2>().enabled = false;
                GetComponent<Phase2Pattern3>().enabled = false;

                GetComponent<Phase2Pattern1>().enabled = true;
                Debug.Log("Pattern " + patternNbr + " has begun");
            }

            else if (patternNbr == 2 && isWaitingForNextPattern == true && !canDoPattern2)
            {
                if (patternCount > 0)
                {
                    GetComponent<Phase2Pattern2>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase2Pattern3>().enabled = false;
                GetComponent<Phase2Pattern1>().enabled = false;

                GetComponent<Phase2Pattern2>().enabled = true;
                Debug.Log("Pattern " + patternNbr + " has begun");
            }

            else if (patternNbr == 3 && isWaitingForNextPattern == true)
            {

                if (patternCount > 0)
                {
                    GetComponent<Phase2Pattern3>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase2Pattern1>().enabled = false;
                GetComponent<Phase2Pattern2>().enabled = false;

                GetComponent<Phase2Pattern3>().enabled = true;
                Debug.Log("Pattern " + patternNbr + " has begun");
            }

        }

        /// <summary>
        /// Tire le prochain pattern aléatoirement, plus le pattern à été joué d'affiler, moins il a de chance d'être tiré.
        /// </summary>
        public void NextPatternSelection()
        {

            for (int i = 0; i < patternCount; i++)
            {
                //patternSelector = Random.Range(1, 4);
                //patternSelector = Random.Range(1, 3);
                patternSelector = 2;

                if (patternSelector != patternNbr)
                {
                    patternCount = 1;
                    break;
                }


            }

            if (patternSelector == 1 && !canDoPattern1)
            {
                patternSelector = 2;
            }

            else if (patternSelector == 2 && !canDoPattern2)
            {
                patternSelector = 1;
            }

            patternNbr = patternSelector;

            isWaitingForNextPattern = true;
        }





    }
}
