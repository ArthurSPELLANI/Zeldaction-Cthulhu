using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Boss
{
    public class Phase1Pattern2 : MonoBehaviour
    {

        private Transform target;
        private GameObject player;

        public int dmg;
        public float attackSpeed;
        public float dashTime;
        public float timeBeforeAttack;
        public float timeBeforeWeakStatusBegin;
        public float timeBeforeWeakStatusEnd;

        private bool canGoToNextPattern;
        private bool canMove;
        private Vector2 vecDir;
        private Rigidbody2D bossPhase1Rb;
        public GameObject phase1Collider;

        [HideInInspector] public bool hasHitWall = false;

        void Awake()
        {
            player = GameObject.Find("Player");
            bossPhase1Rb = GetComponentInParent<Rigidbody2D>();
        }

        void Start()
        {

        }
         
        void OnEnable()
        {
            
            canMove = false;
            canGoToNextPattern = false;
            target = player.transform;
            StartCoroutine(WaitingForPatternStart());
        }

        void Update()
        {
            if (canMove == true)
            {
                bossPhase1Rb.velocity = vecDir * attackSpeed * Time.fixedDeltaTime;
                StartCoroutine(DashDuration());
            }
            else
            {
                bossPhase1Rb.velocity = new Vector2(0,0) * attackSpeed * Time.fixedDeltaTime;
            }

            if (canGoToNextPattern == true)
            {
                Debug.Log("dit moi tout chien");
                GetComponent<Phase1PatternManager>().NextPatternSelection();
            }

            if (hasHitWall == true)
            {
                StopCoroutine(DashDuration());

                phase1Collider.GetComponent<Collider2D>().isTrigger = false;
                canMove = false;

                StartCoroutine(WeakTiming());
            }
        }

        /// <summary>
        /// Set the time before the pattern begin
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitingForPatternStart()
        {

            yield return new WaitForSeconds(timeBeforeAttack);

            vecDir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            canMove = true;

        }

        /// <summary>
        /// Set the duration of the dash so the boss doesn't dash into oblivion.
        /// </summary>
        /// <returns></returns>
        IEnumerator DashDuration()
        {
            phase1Collider.GetComponent<Collider2D>().isTrigger = true;
            yield return new WaitForSeconds(dashTime);

            canMove = false;
            phase1Collider.GetComponent<Collider2D>().isTrigger = false;

            canGoToNextPattern = true;
        }

        /// <summary>
        /// Time during which the boss can be hit
        /// </summary>
        /// <returns></returns>
        IEnumerator WeakTiming()
        {
            yield return new WaitForSeconds(timeBeforeWeakStatusBegin);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

            yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;

            canGoToNextPattern = true;
        }

    }
}
