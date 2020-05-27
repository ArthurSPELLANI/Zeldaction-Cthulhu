﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using AudioManaging;

namespace Enemy
{
	public class DistBehavior : MonoBehaviour
	{

		private GameObject player;

		private Vector2 direction;
		[HideInInspector] public Vector2 projectileDirection;
		private Transform target;
		private Transform projectileTarget;

		private int speed;
		private int enemyCurrentHp;

		public GameObject servantProjectile;

		private Rigidbody2D servantRb;
		public GameObject enemyPrefab;
		public GameObject enemyGraphics;

		public float startAttackRange;
		public float startRetreatRange;
		public float timeBeforeServantFire;

		public float recoilDuration;
		public float maxChaseTime;

		[HideInInspector] public bool canMove = true;
		private bool canDeathSound = true;
		

		public Animator servantAnimator;
		private Vector2 animDirection;

		//private bool canForce = true;
		//private bool forceAttack = false;

		void Awake()
		{
			servantRb = GetComponentInParent<Rigidbody2D>();
			player = GameObject.Find("Player");
			target = player.transform;
			speed = GetComponentInParent<EnemyBasicBehavior>().speed;
		}

		void Start()
		{

		}

		void Update()
		{
			direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
			enemyCurrentHp = GetComponentInParent<EnemyBasicBehavior>().enemyCurrentHealth;

			GetComponentInParent<EnemyBasicBehavior>().SetAnimDirection(direction);
			animDirection = GetComponentInParent<EnemyBasicBehavior>().animDirection;

			servantAnimator.SetFloat("Horizontal", animDirection.x);

			if(canMove == false)
			{
				servantAnimator.SetBool("isRunning", false);
			}
		

			//Si le servant n'est pas à portée d'attaque du joueur et qu'il peut bouger, il avance en direction du joueur.
			if (Vector2.Distance(transform.position, target.position) > startAttackRange && canMove == true)
			{
				servantRb.velocity = direction * speed * Time.fixedDeltaTime;
				servantAnimator.SetBool("isRunning", true);

			}

			//Si le joueur est trop proche du servant, ce dernier s'en éloigne
			else if (Vector2.Distance(transform.position, target.position) < startRetreatRange && canMove == true)
			{
				servantRb.velocity = -direction * speed * Time.fixedDeltaTime;
				servantAnimator.SetBool("isRunning", true);
				
			}

			//Si le joueur est à portée d'attaque du joueur et qu'il peut bouger, il arrête de bouger et lance son attaque.
			else if (Vector2.Distance(transform.position, target.position) < startAttackRange && Vector2.Distance(transform.position, target.position) > startRetreatRange && canMove == true /*|| forceAttack == true*/)
			{
				canMove = false;
				
				servantRb.velocity = new Vector2(0, 0) * speed * Time.fixedDeltaTime;
				servantAnimator.SetBool("isAttacking", true);

				


			}

			//Si l'ennemi n'a plus de pv, son entity est détruite.
			if (enemyCurrentHp <= 0)
			{
				servantAnimator.SetBool("isDiying", true);
				canMove = false;
				StopAllCoroutines();

				if (GetComponentInParent<EnemyBasicBehavior>().isMarked == true)
                {
                    GetComponentInParent<EnemyBasicBehavior>().SanityReward();
                }
				
				if(canDeathSound)
				{
					//son
					canDeathSound = false;
					AudioManager.Instance.Play("mortRanged");
				}

				
			}
		}


		/// <summary>
		/// The Servant fire a projectile after a short delay and then wait for another short delay.
		/// </summary>
		/// <returns></returns>
		public IEnumerator FireProjectile()
		{
			//son
			AudioManager.Instance.Play("attackRanged");

			Instantiate(servantProjectile, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(recoilDuration);

			servantAnimator.SetBool("isAttacking", false);
			canMove = true;
		}

		public void CancelAllCoroutines()
		{
			StopAllCoroutines();
		}

		/*IEnumerator EndChase()
		{
			yield return new WaitForSeconds(maxChaseTime);

			forceAttack = true;
			canForce = true;
		}*/

	}
}
