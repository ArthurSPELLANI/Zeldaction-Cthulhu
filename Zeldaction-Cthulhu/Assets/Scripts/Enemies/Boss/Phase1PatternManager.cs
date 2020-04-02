using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Phase1PatternManager : MonoBehaviour
    {
        private int patternNbr;
        private int patternSelector;
        private int patternCount;
        private bool isWaitingForNextPattern = true;
        
        void Awake()
        {
            patternNbr = 1;
        }

        void Start()
        {

        }

        void Update()
        {
            if(patternNbr == 1 && isWaitingForNextPattern == true)
            {
                if (patternCount > 0)
                {
                    Debug.Log("alo ?");
                    GetComponent<Phase1Pattern1>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase1Pattern2>().enabled = false;
                GetComponent<Phase1Pattern3>().enabled = false;

                GetComponent<Phase1Pattern1>().enabled = true;
            }

            else if(patternNbr == 2 && isWaitingForNextPattern == true)
            {
                if (patternCount > 0)
                {
                    GetComponent<Phase1Pattern2>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase1Pattern3>().enabled = false;
                GetComponent<Phase1Pattern1>().enabled = false;

                GetComponent<Phase1Pattern2>().enabled = true;
            }

            else if (patternNbr == 3 && isWaitingForNextPattern == true)
            {

                if (patternCount > 0)
                {
                    GetComponent<Phase1Pattern3>().enabled = false;
                }

                isWaitingForNextPattern = false;
                patternCount++;

                GetComponent<Phase1Pattern1>().enabled = false;
                GetComponent<Phase1Pattern2>().enabled = false;

                GetComponent<Phase1Pattern3>().enabled = true;
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
                patternSelector = 2;

                if (patternSelector != patternNbr)
                {
                    patternCount = 0;
                    break;
                }
            }

            patternNbr = patternSelector;
            isWaitingForNextPattern = true;
        }





    }
}
