using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Shadow;
using AudioManaging;
using Game;
using UI;

namespace Enemy
{
	public class EnemyBasicBehavior : MonoBehaviour
	{
        public int enemyMaxHealth;
        public int enemyCurrentHealth;
        public int enemyDamage;

        public GameObject fieldOfView;
        public GameObject preDetectionPath;
        private GameObject behavior;
        private int childNbr;
        public Transform[] path;
        private int currentWaypoint = 0;
        [HideInInspector] public Vector2 direction;
        private Rigidbody2D EnemyRb;

        [Range(0, 100)]
        public int speed;

        public float sanityDamage;
        public bool isMarked;
        public GameObject markedParticle;
        public float sanityReward;
        public float timestampMark;
        public float coolDownMark = 5f;

        private double currentWaypointXMin;
        private double currentWaypointXMax;
        private double currentWaypointYMin;
        private double currentWaypointYMax;

        [HideInInspector] public bool canMove = true;

        private bool isThrown;
        private Vector2 vecThrow;

        public Animator enemyAnimator;
        [HideInInspector] public Vector2 animDirection;
        [SerializeField] public Animator catchAnimator;
        SpriteRenderer catchSprite;

        public float randomIdleChance;
        private bool timerHasStarted = false;
        public float timeBeforeRandomIdle;
        private float timerRI;
        [HideInInspector] public bool isDoingRI;

        Material defaultMaterial;

        public float knockbackDuration;
        public AnimationCurve knockbackForceModifier;

        public bool isStunned = false;
        private float timestampStun;
        public float HitStunDuration;

        public float loseAggroDistance;
        private float distanceToPlayer;
        private Transform playerTransform;
        private float timestampAggro;
        public float timeBeforeLoseAggro;
        private bool losingPlayer = false;


        void Awake()
		{
            enemyCurrentHealth = enemyMaxHealth;
            childNbr = preDetectionPath.transform.childCount;
            EnemyRb = GetComponent<Rigidbody2D>();
            behavior = transform.GetChild(2).gameObject;
            catchAnimator = GetComponent<Animator>();
            catchSprite = GetComponent<SpriteRenderer>();
            defaultMaterial = GetComponentInChildren<SpriteRenderer>().material;

            playerTransform = PlayerManager.Instance.transform;
        }

		void Start()
		{
            LookingForPath();
        }

		void Update()
		{
            if(timestampStun <= Time.time && isStunned == true)
            {
                isStunned = false;
                canMove = true;

                if (enemyCurrentHealth > 0)
                {
                    if (behavior.name == "WolfBehavior")
                    {
                        behavior.GetComponent<WolfBehavior>().canMove = true;
                    }
                    else if (behavior.name == "DistBehavior")
                    {
                        behavior.GetComponent<DistBehavior>().canMove = true;
                    }
                }

                if (fieldOfView.GetComponent<PlayerDetection>().isDetected == false)
                {
                    fieldOfView.GetComponent<PlayerDetection>().isDetected = true;
                }                
            }

            if(timestampMark <= Time.time)
            {
                isMarked = false;
            }

            if(isStunned == false)
            {
                if (childNbr > 0)
                {
                    if (fieldOfView.GetComponent<PlayerDetection>().isDetected == false)
                    {
                        direction = new Vector2(path[currentWaypoint].position.x - transform.position.x, path[currentWaypoint].position.y - transform.position.y).normalized;
                        SetAnimDirection(direction);

                        enemyAnimator.SetFloat("Horizontal", animDirection.x);


                        //Soft spot around the Waypoint position cause rigidbody can't reach a precise position while using velocity to move.
                        currentWaypointXMin = path[currentWaypoint].position.x - 0.02;
                        currentWaypointXMax = path[currentWaypoint].position.x + 0.02;
                        currentWaypointYMin = path[currentWaypoint].position.y - 0.02;
                        currentWaypointYMax = path[currentWaypoint].position.y + 0.02;

                        //if the enemy as reached the Waypoint, he target the next one on the Array.
                        if (transform.position.x >= currentWaypointXMin && transform.position.x <= currentWaypointXMax && transform.position.y >= currentWaypointYMin && transform.position.y <= currentWaypointYMax)
                        {
                            StartCoroutine(ChangingWaypoint());
                        }
                        //If the enemy as not reached the Waypoint yet, he goes towards it.
                        else if (canMove == true)
                        {
                            EnemyRb.velocity = direction * speed * Time.fixedDeltaTime;
                        }

                    }
                    //Tout le code qui suit gère l'aggro du mob 
                    else
                    {
                        if(enemyCurrentHealth > 0)
                        {
                            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

                            if (distanceToPlayer > loseAggroDistance)
                            {
                                fieldOfView.GetComponent<PlayerDetection>().isDetected = false;
                                fieldOfView.GetComponent<PolygonCollider2D>().enabled = true;

                                #region Stop current attack if the player leave aggro during enemy attack
                                if (behavior.name == "WolfBehavior")
                                {
                                    behavior.GetComponent<WolfBehavior>().CancelAllCouritines();

                                    behavior.GetComponent<WolfBehavior>().canMove = true;
                                    canMove = true;
                                    behavior.GetComponent<WolfBehavior>().isAttacking = false;
                                    behavior.GetComponent<WolfBehavior>().wolfAnimator.SetBool("isAttacking", false);

                                    GetComponent<CapsuleCollider2D>().isTrigger = false;
                                    behavior.GetComponent<CircleCollider2D>().isTrigger = false;

                                }
                                else if (behavior.name == "DistBehavior")
                                {
                                    behavior.GetComponent<DistBehavior>().CancelAllCoroutines();

                                    behavior.GetComponent<DistBehavior>().canMove = true;
                                    canMove = true;
                                    behavior.GetComponent<DistBehavior>().servantAnimator.SetBool("isAttacking", false);
                                }
                                #endregion

                                behavior.SetActive(false);
                                losingPlayer = false;


                            }
                            else if (distanceToPlayer > loseAggroDistance / 1.5f && distanceToPlayer < loseAggroDistance)
                            {
                                if (!losingPlayer)
                                {
                                    timestampAggro = Time.time + timeBeforeLoseAggro;
                                    losingPlayer = true;
                                }

                                if (timestampAggro <= Time.time)
                                {
                                    fieldOfView.GetComponent<PlayerDetection>().isDetected = false;
                                    fieldOfView.GetComponent<PolygonCollider2D>().enabled = true;
                                    behavior.SetActive(false);
                                    losingPlayer = false;
                                }
                            }
                        
                        }
                        else
                        {
                            losingPlayer = false;
                        }
                    }

                    if (canMove == true)
                    {
                        enemyAnimator.SetBool("isRunning", true);

                        if (behavior.name != "DistBehavior")
                            enemyAnimator.SetBool("isRandomIdle", false);

                        timerHasStarted = false;
                        isDoingRI = false;
                    }
                    else
                    {
                        enemyAnimator.SetBool("isRunning", false);
                        
                        if (behavior.name != "DistBehavior")
                        {
                            if (timerHasStarted == false && isDoingRI == false)
                            {
                                timerRI = timeBeforeRandomIdle;
                                timerHasStarted = true;
                            }

                            if (timerHasStarted == true)
                            {
                                timerRI -= Time.deltaTime;
                            }

                            if (timerRI <= 0 && timerHasStarted == true && isDoingRI == false)
                            {
                                if (Random.Range(0f, 1f) < randomIdleChance)
                                {
                                    enemyAnimator.SetBool("isRandomIdle", true);
                                    isDoingRI = true;
                                }

                                timerHasStarted = false;
                            }
                        }
                        
                    }
                }
            }

		}




        /// <summary>
        /// Reduce enemy health by player attack damage.
        /// </summary>
        /// <param name="playerDamage"></param>
        public void TakeDamage(int playerDamage, Vector3 sourcePos, float pushForce)
        {
            if (enemyCurrentHealth > 0)
            {
                enemyCurrentHealth -= playerDamage;

                //Si le joueur n'est pas encore detecter par l'enemy, il le devient.
                if (fieldOfView.GetComponent<PlayerDetection>().isDetected == false)
                {
                    fieldOfView.GetComponent<PlayerDetection>().isDetected = true;
                }

                StartCoroutine(Knockback(sourcePos, pushForce));
                StartCoroutine(hitFrames());


                //son de prise de dégâts
                if (gameObject.transform.parent.gameObject.CompareTag("Loup") && enemyCurrentHealth > 0)
                {
                    AudioManager.Instance.Play("priseDeDegatsLoup");
                }
                else if (gameObject.transform.parent.gameObject.CompareTag("Range") && enemyCurrentHealth > 0)
                {
                    AudioManager.Instance.Play("priseDeDegatsRanged");
                }
            }

        }


        /// <summary>
        /// Create a new Array of the same size as their are children in PreDetectionPath 
        /// and then put those children into the Array in order.
        /// </summary>
        private void LookingForPath()
        {
            path = new Transform[childNbr];

            for (int i = 0; i < childNbr; i++)
            {
                path[i] = preDetectionPath.transform.GetChild(i);
            }

        }


        /// <summary>
        /// When the enemy reach a Waypoint it stops for x seconds and change target Waypoint
        /// </summary>
        /// <returns></returns>
        IEnumerator ChangingWaypoint()
        {
            canMove = false;
            EnemyRb.velocity = direction * 0 * Time.fixedDeltaTime;

            currentWaypoint++;

            //return at the start of the Array.
            if (currentWaypoint == childNbr)
            {
                currentWaypoint = 0;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            canMove = true;
        }

        public void SetAnimDirection(Vector2 direction)
        {
            if (direction.x > 0)
            {
                animDirection.x = 1;
            }
            else
            {
                animDirection.x = -1;
            }
        }

        public void SanityDamage()
        {
            PlayerManager.Instance.playerShadowMode.sanity -= sanityDamage;
            
            if(PlayerManager.Instance.playerShadowMode.sanity < 0)
            {
                PlayerManager.Instance.playerShadowMode.sanity = 0;
                UIManager.Instance.GetComponentInChildren<UISanityGauge>().SetSanity(PlayerManager.Instance.playerShadowMode.sanity);
            }
        }

        
        public void SanityReward()
        {
            PlayerManager.Instance.playerShadowMode.sanity += sanityReward;
            
            if(PlayerManager.Instance.playerShadowMode.sanity > PlayerManager.Instance.playerShadowMode.maxSanity)
            {
                PlayerManager.Instance.playerShadowMode.sanity = PlayerManager.Instance.playerShadowMode.maxSanity;
                UIManager.Instance.GetComponentInChildren<UISanityGauge>().SetSanity(PlayerManager.Instance.playerShadowMode.sanity);
            }
        }


        /// <summary>
        /// USED IN PHASE 2 PATTERN 1 OF BOSS : Desactivate player detection, throw the enemy in a semi-random position and then force player detection.
        /// </summary>
        /// <param name="throwTime"></param>
        /// <returns></returns>
        public IEnumerator bossThrowState(float throwTime, float throwSpeed)
        {
            fieldOfView.SetActive(false);
            GetComponent<Collider2D>().isTrigger = true;

            vecThrow = new Vector2(Random.Range(-0.1f, 0.9f), Random.Range(-1f, -0.5f)).normalized;
            EnemyRb.velocity = vecThrow * (throwSpeed * Random.Range(0.5f, 0.8f)) * Time.deltaTime;

            yield return new WaitForSeconds(throwTime);

            EnemyRb.velocity = new Vector2(0, 0) * 0 * Time.deltaTime;
            fieldOfView.SetActive(true);
            fieldOfView.GetComponent<PlayerDetection>().isDetected = true;
        }

        public void EnemyStun(float coolDownStun)
        {
            isStunned = true;
            canMove = false;
            timestampStun = Time.time + coolDownStun;
            EnemyRb.velocity = Vector2.zero;

            if (behavior.name == "WolfBehavior")
            {
                behavior.GetComponent<WolfBehavior>().CancelAllCouritines();

                behavior.GetComponent<WolfBehavior>().canMove = false;
                behavior.GetComponent<WolfBehavior>().isAttacking = false;
                behavior.GetComponent<WolfBehavior>().wolfAnimator.SetBool("isAttacking", false);

                GetComponent<CapsuleCollider2D>().isTrigger = false;
                behavior.GetComponent<CircleCollider2D>().isTrigger = false;
                
            }
            else if (behavior.name == "DistBehavior")
            {
                behavior.GetComponent<DistBehavior>().CancelAllCoroutines();

                behavior.GetComponent<DistBehavior>().canMove = false;
                behavior.GetComponent<DistBehavior>().servantAnimator.SetBool("isAttacking", false);
            }
            else if (behavior.name == "ExploBehavior")
            {
                behavior.GetComponent<ExploBehavior>().canMove = false;
            }
            
        }


        IEnumerator Knockback(Vector3 sourcePos, float pushForce)
        {
            float timer = 0.0f;
            GetComponent<CapsuleCollider2D>().isTrigger = true;

            //pour que le knockback soit moins violent à la mort de l'ennemi
            if (enemyCurrentHealth <= 0)
            {
                pushForce /= 4;
            }

            while (timer < knockbackDuration)
            {
                EnemyRb.velocity = (transform.position - sourcePos).normalized * pushForce * knockbackForceModifier.Evaluate(timer / knockbackDuration);
                timer += Time.deltaTime;

                yield return null;
            }

            GetComponent<CapsuleCollider2D>().isTrigger = false;

            if (enemyCurrentHealth > 0)
            {
                if (!isStunned)
                {
                    EnemyStun(HitStunDuration);
                }
                else
                {
                    EnemyStun(HitStunDuration / 2);
                }
            }
            
        }

        IEnumerator hitFrames()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/White");
            yield return new WaitForSeconds(0.05f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.05f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/Black");
            yield return new WaitForSeconds(0.05f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = defaultMaterial;
        }

        public void MarkingCoolDown()
        {
            isMarked = true;
            timestampMark = Time.time + coolDownMark;
            Instantiate(markedParticle, transform);
        }

        public void ExitCatchAnimator()
        {
            catchAnimator.SetBool("isDetected", false);
            catchAnimator.SetBool("isDesactivated", false);
            catchAnimator.enabled = false;
            catchSprite.sprite = null;
        }

        public void CatchByShadow()
        {
            catchAnimator.SetBool("isDetected", true);
        }

        public void CatchOut()
        {
            catchAnimator.SetBool("isDesactivated", true);
        }

        private void OnDrawGizmosSelected()
        {
            
            if(fieldOfView.GetComponent<PlayerDetection>().isDetected == true && enemyCurrentHealth > 0)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, loseAggroDistance);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, loseAggroDistance / 1.5f);
            }
            
        }

    }
}