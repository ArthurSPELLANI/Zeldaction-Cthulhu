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
        public float timeBeforePatternEnd;
        public float timeBeforeWeakStatusStart;
        public float timeBeforeWeakStatusEnd;

        public float throwSpeed;
        public float throwTime;

        private bool part2CanBegin = false;
        private int childNbr;
        private bool blockState;
        private bool gotHit;

        private GameObject graphics;

        public Animator animator;

        void Awake()
        {
            wolfRb = wolf.GetComponentInChildren<Rigidbody2D>();
            servantRb = servant.GetComponentInChildren<Rigidbody2D>();
            failedServantRb = failedServant.GetComponentInChildren<Rigidbody2D>();
            spawnParent = GameObject.Find("SpawnParent");
            spawnPoint = GameObject.Find("SpawnPoint");
            graphics = transform.parent.GetChild(0).gameObject;
        }

        void Start()
        {

        }

        void OnEnable()
        {
            spawnNbr = Random.Range(spawnNbrMin, spawnNbrMax);
            spawnList = new GameObject[spawnNbr];

            blockState = false;

            StartCoroutine(PatternStart());
        }

        void Update()
        {
            if (part2CanBegin == true)
            {
                childNbr = spawnParent.transform.childCount;

                if (childNbr > 0 && gotHit == false)
                {
                    blockState = true;
                    graphics.GetComponent<SpriteRenderer>().color = Color.green;    
                }

                else if (childNbr == 0 && gotHit == false)
                {
                    part2CanBegin = false;
                    StartCoroutine(EndOfPattern());                    
                }

                else if (gotHit == true)
                {
                    part2CanBegin = false;
                    StartCoroutine(GetExploded());
                }


            }
        }


        /// <summary>
        /// Spawn a random amount of enemies, including a faibled servant and then throw them in front of the boss.
        /// </summary>
        /// <returns></returns>
        IEnumerator PatternStart()
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

            part2CanBegin = true;

        }

      
        /// <summary>
        /// Move to next pattern after a small "recoil"
        /// </summary>
        /// <returns></returns>
        IEnumerator EndOfPattern()
        {
            graphics.GetComponent<SpriteRenderer>().color = Color.white;

            yield return new WaitForSeconds(timeBeforePatternEnd);

            GetComponent<Phase2PatternManager>().NextPatternSelection();
        }

        /// <summary>
        /// Set the time during which the boss can get hit by the player.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetExploded()
        {

            gotHit = true;
            blockState = false;

            yield return new WaitForSeconds(timeBeforeWeakStatusStart);

            animator.SetBool("isVul", true);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

            yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

            animator.SetBool("isVul", false);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;
                
            //Assure we leave pattern in update function by removing all enemies and setting gotHit to false.
            foreach (GameObject enemy in spawnList)
            {
                GameObject.Destroy(enemy);
            }
            gotHit = false;


            part2CanBegin = true;

        }


        /// <summary>
        /// return gotHit = true after boss get hit by failed servant explosion.
        /// </summary>
        public void ExploHitBoss()
        {
            if (blockState == true)
            {
                gotHit = true;
            }
        }

    }
}

