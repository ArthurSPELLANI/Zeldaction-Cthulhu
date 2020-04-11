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
        private GameObject spawnPoint;
        private GameObject spawnParent;

        public GameObject[] spawnList;

        private Rigidbody2D wolfRb;
        private Rigidbody2D servantRb;
        private Rigidbody2D failedServantRb;

        private int spawnNbr;
        public int spawnNbrMin;
        public int spawnNbrMax;

        public float timeBeforePatternStart;

        public float throwSpeed;
        public float throwTime;


        void Awake()
        {
            wolfRb = wolf.GetComponentInChildren<Rigidbody2D>();
            servantRb = servant.GetComponentInChildren<Rigidbody2D>();
            failedServantRb = failedServant.GetComponentInChildren<Rigidbody2D>();
            spawnParent = GameObject.Find("SpawnParent");
            spawnPoint = GameObject.Find("SpawnPoint");
        }

        void Start()
        {

        }

        void OnEnable()
        {
            spawnNbr = Random.Range(spawnNbrMin, spawnNbrMax);
            spawnList = new GameObject[spawnNbr];
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
                        Instantiate(wolf, spawnPoint.transform.position, Quaternion.identity, spawnParent.transform);
                    } 
                    else if (selecVar == 2)
                    {
                        Instantiate(servant, spawnPoint.transform.position, Quaternion.identity, spawnParent.transform);
                    }

                }
                else
                {
                    Instantiate(failedServant, spawnPoint.transform.position, Quaternion.identity, spawnParent.transform);
                }

                spawnList[i] = spawnParent.transform.GetChild(i).gameObject;
            }

            foreach (GameObject enemy in spawnList)
            {
                StartCoroutine(enemy.GetComponentInChildren<EnemyBasicBehavior>().bossThrowState(throwTime, throwSpeed));
            }

        }

      

    }
}

