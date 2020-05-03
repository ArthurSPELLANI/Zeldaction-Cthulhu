using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Shadow;

namespace Enemy
{
	public class EnemyBasicBehavior : MonoBehaviour
	{
        public int enemyMaxHealth;
        [SerializeField] public int enemyCurrentHealth;
        public int enemyDamage;

        public GameObject fieldOfView;
        public GameObject preDetectionPath;
        private int childNbr;
        public Transform[] path;
        private int currentWaypoint = 0;
        [HideInInspector] public Vector2 direction;
        private Rigidbody2D EnemyRb;

        [Range(0, 100)]
        public int speed;

        public float sanityDamage;
        public bool isMarked;
        public float sanityReward;
        public float timestampMark;
        public float coolDownMark = 5f;

        private double currentWaypointXMin;
        private double currentWaypointXMax;
        private double currentWaypointYMin;
        private double currentWaypointYMax;

        [HideInInspector] public bool canMove = true;
        //private bool canScratch = true;

        private bool isThrown;
        private Vector2 vecThrow;

        public Animator enemyAnimator;
        [HideInInspector]public Vector2 animDirection;

        public float scratchChance;

        public float knockbackDuration;
        public AnimationCurve knockbackForceModifier;

        public bool isStunned = false;
        float timestampStun;
        float coolDownStun = 3f;

        void Awake()
		{
            enemyCurrentHealth = enemyMaxHealth;
            childNbr = preDetectionPath.transform.childCount;
            EnemyRb = GetComponent<Rigidbody2D>();
        }

		void Start()
		{
            LookingForPath();
        }

		void Update()
		{
            if(timestampStun <= Time.time)
            {
                isStunned = false;
                canMove = true;
                if(fieldOfView.GetComponent<PlayerDetection>().isDetected == true)
                {
                    fieldOfView.GetComponent<PlayerDetection>().behavior.SetActive(true);
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
                    if (canMove == true)
                    {
                        enemyAnimator.SetBool("isRunning", true);
                    }
                    else
                    {
                        enemyAnimator.SetBool("isRunning", false);
                        /* if (canScratch == true)
                         {
                             canScratch = false;
                             //mettre la coroutine
                         }
                         */
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
            
            enemyCurrentHealth -= playerDamage;

            if (fieldOfView.GetComponent<PlayerDetection>().isDetected == false)
            {
                fieldOfView.GetComponent<PlayerDetection>().isDetected = true;
            }

            StartCoroutine(Knockback(sourcePos, pushForce));

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
        }

        public void SanityReward()
        {
            PlayerManager.Instance.playerShadowMode.sanity += sanityReward;
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

        public void EnemyStun()
        {
            isStunned = true;
            canMove = false;
            timestampStun = Time.time + coolDownStun;
            EnemyRb.velocity = new Vector2(0, 0);
            fieldOfView.GetComponent<PlayerDetection>().behavior.SetActive(false);
        }


        IEnumerator Knockback(Vector3 sourcePos, float pushForce)
        {
            float timer = 0.0f;

            while (timer < knockbackDuration)
            {
                EnemyRb.velocity = (transform.position - sourcePos) * pushForce * knockbackForceModifier.Evaluate(timer / knockbackDuration);
                timer += Time.deltaTime;

                //Animation de prise de dégats ici.

                yield return null;
            }

            EnemyRb.velocity = new Vector2(0, 0) * 0 * Time.deltaTime;
        }

        public void MarkingCoolDown()
        {
            isMarked = true;
            timestampMark = Time.time + coolDownMark;
        }
    


    }
}