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
                GetComponent<Phase1PatternManager>().NextPatternSelection();
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
        /// Set theduration of the dash so the boss doesn't dash into oblivion.
        /// </summary>
        /// <returns></returns>
        IEnumerator DashDuration()
        {
            yield return new WaitForSeconds(dashTime);

            canMove = false;
        }

        IEnumerator WeakTiming()
        {

            yield return new WaitForSeconds(timeBeforeWeakStatusBegin);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

            yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;

            canGoToNextPattern = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("player"))
            {
                other.GetComponent<PlayerStats>().PlayerTakeDamage(dmg);
            }

            if (other.CompareTag("Enviro"))
            {
                canMove = false;
                StartCoroutine(WeakTiming());
            }
        }

    }
}
