using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Boss
{
    public class Phase2Pattern1 : MonoBehaviour
    {
        public GameObject wolf;
        public GameObject servant;
        public GameObject failedServant;
        public GameObject spawnPoint;

        private Rigidbody2D wolfRb;
        private Rigidbody2D servantRb;
        private Rigidbody2D failedServantRb;

        private int spawnNbr;
        public int spawnNbrMin;
        public int spawnNbrMax;

        public float timeBeforePatternStart;


        void Awake()
        {
            wolfRb = wolf.GetComponentInChildren<Rigidbody2D>();
            servantRb = servant.GetComponentInChildren<Rigidbody2D>();
            failedServantRb = failedServant.GetComponentInChildren<Rigidbody2D>();
        }

        void Start()
        {

        }

        void OnEnable()
        {
            spawnNbr = Random.Range(spawnNbrMin, spawnNbrMax);
            StartCoroutine(Pattern());
        }

        void Update()
        {

        }

        IEnumerator Pattern()
        {
            yield return new WaitForSeconds(timeBeforePatternStart);

            for (int i = 0; i < spawnNbr; i++)
            {
                if (i < spawnNbr - 1)
                {
                    int selecVar = Random.Range(1, 3);

                    if (selecVar == 1)
                    {
                        Instantiate(wolf, spawnPoint.transform.position, Quaternion.identity);
                        wolf.GetComponentInChildren<EnemyBasicBehavior>().bossThrow();
                    } 
                    else if (selecVar == 2)
                    {
                        Instantiate(servant, spawnPoint.transform.position, Quaternion.identity);
                        servant.GetComponentInChildren<EnemyBasicBehavior>().bossThrow();
                    }

                }
                else
                {
                    Instantiate(failedServant, spawnPoint.transform.position, Quaternion.identity);
                    servant.GetComponentInChildren<EnemyBasicBehavior>().bossThrow();
                }
            }


        }
    }
}

