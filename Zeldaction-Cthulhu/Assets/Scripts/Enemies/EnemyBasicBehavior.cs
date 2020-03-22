﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class EnemyBasicBehavior : MonoBehaviour
	{
        public int enemyMaxHealth;
        [SerializeField] int enemyCurrentHealth;
        public int enemyDamage;

        public GameObject fieldOfView;
        public GameObject preDetectionPath;
        private int childNbr;
        public Transform[] path;
        private int currentWaypoint = 0;
        /*[HideInInspector]*/ public Vector2 direction;
        private Rigidbody2D EnemyRb;

        [Range(0, 100)]
        public int speed;

        private double currentWaypointXMin;
        private double currentWaypointXMax;
        private double currentWaypointYMin;
        private double currentWaypointYMax;

        private bool canMove = true;

        public Animator enemyAnimator;
        [HideInInspector]public Vector2 animDirection;

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
                    if (transform.position.x >= currentWaypointXMin && transform.position.x <= currentWaypointXMax && transform.position.y >= currentWaypointYMin && transform.position.y >= currentWaypointYMin)
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
                }
            }

		}



        /// <summary>
        /// Reduce enemy health by player attack damage.
        /// </summary>
        /// <param name="playerDamage"></param>
        public void TakeDamage(int playerDamage)
        {
            enemyCurrentHealth -= playerDamage;

            if(enemyCurrentHealth <= 0)
            {
                Destroy(gameObject);
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
            Debug.Log("currentWaypoint is : " + currentWaypoint);

            //return at the start of the Array.
            if (currentWaypoint == childNbr)
            {
                currentWaypoint = 0;
            }

            yield return new WaitForSeconds(0.3f);
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

    }
}