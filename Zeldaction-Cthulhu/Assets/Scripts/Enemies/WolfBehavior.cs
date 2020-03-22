﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
	public class WolfBehavior : MonoBehaviour
	{

		private GameObject player;
		private Vector2 direction;
		private Vector2 dashDirection;
		private Transform target;
		private Transform dashTarget;
		private int speed;
		private int enemyDamage;

		private Rigidbody2D wolfRb;
		public GameObject enemyPrefab;
		public GameObject enemyGraphics;

		public float startAttackRange;
		public float timeBeforeTargetLock;
		public float timeBeforeWolfAttack;
		[Range(0, 200)]
		public int attackSpeed;
		public float recoilDuration;

		private bool canMove = true;

		public Animator wolfAnimator;
		private Vector2 animDirection;

		void Awake()
		{
			wolfRb = GetComponentInParent<Rigidbody2D>();
			player = GameObject.Find("Player");
			target = player.transform;
			speed = enemyPrefab.GetComponent<EnemyBasicBehavior>().speed;
			enemyDamage = enemyPrefab.GetComponent<EnemyBasicBehavior>().enemyDamage;
			
		}

		void Start()
		{
			
		}

		void Update()
		{
			direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;

			
			

			

			//Si le wolf n'est pas à portée d'attaque du joueur et qu'il peut bouger, il avance en direction du joueur.
			if (Vector2.Distance(transform.position, target.position) > startAttackRange && canMove == true)
			{
				wolfRb.velocity = direction * speed * Time.fixedDeltaTime;

				wolfAnimator.SetBool("isRunning", true);

				enemyPrefab.GetComponent<EnemyBasicBehavior>().SetAnimDirection(direction);
				animDirection = enemyPrefab.GetComponent<EnemyBasicBehavior>().animDirection;
				wolfAnimator.SetFloat("Horizontal", animDirection.x);
			}

			//Si le joueur est à portée d'attaque du joueur et qu'il peut bouger, il arrête de bouger et lance son attaque.
			else if (Vector2.Distance(transform.position, target.position) < startAttackRange && canMove == true)
			{
				canMove = false;
				wolfRb.velocity = new Vector2(0,0) * speed * Time.fixedDeltaTime;
				wolfAnimator.SetBool("isRunning", false);
				StartCoroutine(WolfAttack());
			}


		}


		/// <summary>
		/// Wolf Attack Pattern
		/// </summary>
		/// <returns></returns>
		IEnumerator WolfAttack()
		{
			enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.red;
			

			yield return new WaitForSeconds(timeBeforeTargetLock);

			dashTarget = target;
			dashDirection = new Vector2(dashTarget.position.x - transform.position.x, dashTarget.position.y - transform.position.y).normalized;
			enemyPrefab.GetComponent<EnemyBasicBehavior>().SetAnimDirection(dashDirection);
			animDirection = enemyPrefab.GetComponent<EnemyBasicBehavior>().animDirection;

			yield return new WaitForSeconds(timeBeforeWolfAttack);

			wolfAnimator.SetFloat("Horizontal", animDirection.x);
			wolfAnimator.SetBool("isAttacking", true);
			GetComponent<BoxCollider2D>().isTrigger = true;
			wolfRb.velocity = dashDirection * attackSpeed * Time.fixedDeltaTime;
			

			yield return new WaitForSeconds(recoilDuration);

			GetComponent<BoxCollider2D>().isTrigger = false;
			enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.white;
			canMove = true;
			wolfAnimator.SetBool("isAttacking", false);

			
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<PlayerStats>().PlayerTakeDamage(enemyDamage);
			}
		}

	}
}
