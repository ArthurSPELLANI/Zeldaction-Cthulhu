using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using AudioManaging;

namespace Boss
{
    public class Phase1Pattern2 : MonoBehaviour
    {

        private Transform target;
        private GameObject player;

        public int dmg;

        public float attackSpeed;
        public AnimationCurve attackSpeedMofifier;
        public float dashTime;

        public float timeBeforeAttack;
        public float timeBeforePatternEnd;

        public float bounceSpeed;
        public AnimationCurve bounceSpeedModifier;
        public float bounceTime;

        public float timeBeforeWeakStatusBegin;
        public float timeBeforeWeakStatusEnd;

        private Vector2 vecDir;
        Vector2 vecAnim;
        private Rigidbody2D bossPhase1Rb;
        public GameObject phase1Collider;

        public Animator animator;

        [HideInInspector] public bool hasHitWall = false;

        void Awake()
        {
            player = GameObject.Find("Player");
            bossPhase1Rb = GetComponentInParent<Rigidbody2D>();
        }

        void OnEnable()
        {
            target = player.transform;
            StartCoroutine(WaitingForPatternStart());
        }

        void Update()
        {
            if (hasHitWall == true)
            {
                hasHitWall = false;
                animator.SetBool("hitByWall", true);
                StopAllCoroutines();

                bossPhase1Rb.velocity = new Vector2(0, 0) * attackSpeed * Time.fixedDeltaTime;
                phase1Collider.GetComponent<Collider2D>().isTrigger = false;

                StartCoroutine(BounceIntoWeak());
                AudioManager.Instance.Stop("idleShadow");
                AudioManager.Instance.Play("DegatM1");
            }
        }

        /// <summary>
        /// Set the time before the pattern begin
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitingForPatternStart()
        {
            animator.SetBool("isCharging", true);
            AudioManager.Instance.Play("Tentacl");
            AudioManager.Instance.Play("idleShadow");
            AudioManager.Instance.Play("Tentacules");

            yield return new WaitForSeconds(timeBeforeAttack);

            vecDir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;

            StartCoroutine(Dash());

        }

        /// <summary>
        /// Set the duration of the dash so the boss doesn't dash into oblivion.
        /// </summary>
        /// <returns></returns>
        IEnumerator Dash()
        {
            float timer = 0.0f;

            phase1Collider.GetComponent<Collider2D>().isTrigger = true;

            while (timer < dashTime)
            {
                bossPhase1Rb.velocity = vecDir * attackSpeed * attackSpeedMofifier.Evaluate(timer / dashTime);                
                SetAnimDirection(vecDir);
                animator.SetFloat("Horizontal", vecAnim.x);

                timer += Time.deltaTime;

                yield return null;
            }

            bossPhase1Rb.velocity = new Vector2(0, 0) * attackSpeed * Time.fixedDeltaTime;          
            phase1Collider.GetComponent<Collider2D>().isTrigger = false;

            yield return new WaitForSeconds(timeBeforePatternEnd);

            animator.SetBool("isCharging", false);

            Debug.Log("aled");
            GetComponent<Phase1PatternManager>().NextPatternSelection();
        }

        /// <summary>
        /// Bounce effect + Time during which the boss can be hit 
        /// </summary>
        /// <returns></returns>
        IEnumerator BounceIntoWeak()
        {
            StopCoroutine(Dash());
            animator.SetBool("hitByWall", true);
            animator.SetBool("isCharging", false);
            AudioManager.Instance.Stop("idleShadow");

            hasHitWall = false;

            float timer = 0.0f;

            while (timer < bounceTime)
            {
                bossPhase1Rb.velocity = -vecDir * bounceSpeed * bounceSpeedModifier.Evaluate(timer / bounceTime);

                timer += Time.deltaTime;

                yield return null;
            }

            bossPhase1Rb.velocity = new Vector2(0, 0) * attackSpeed * Time.fixedDeltaTime;

            yield return new WaitForSeconds(timeBeforeWeakStatusBegin);

            animator.SetBool("isWeak", true);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;
            AudioManager.Instance.Play("Fatigue");


            yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

            animator.SetBool("isWeak", false);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;

            yield return new WaitForSeconds(timeBeforePatternEnd);

            animator.SetBool("hitByWall", false);

            GetComponent<Phase1PatternManager>().NextPatternSelection();
            AudioManager.Instance.Stop("Tentacules");
        }


        public void SetAnimDirection(Vector2 vecDir)
        {
            if (vecDir.x > 0)
            {
                vecAnim.x = 1;
            }
            else
            {
                vecAnim.x = -1;
            }
        }

    }
}
