using System.Collections;
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
		private int enemyCurrentHp;

		private Rigidbody2D wolfRb;
		public GameObject enemyPrefab;
		public GameObject enemyGraphics;

		public float startAttackRange;
		public float timeBeforeTargetLock;
		public float timeBeforeWolfAttack;
		[Range(0, 200)]
		public int attackSpeed;
		public AnimationCurve attackSpeedMofifier;
		public float attackDuration;
		public float recoilDuration;

		public bool isAttacking;

		private bool canMove = true;

		public Animator wolfAnimator;
		private Vector2 animDirection;

		void Awake()
		{
			wolfRb = GetComponentInParent<Rigidbody2D>();
			player = GameObject.Find("Player");
			target = player.transform;
			speed = GetComponentInParent<EnemyBasicBehavior>().speed;
			enemyDamage = GetComponentInParent<EnemyBasicBehavior>().enemyDamage;
		}

		void Start()
		{
			
		}

		void Update()
		{
			direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;

			enemyCurrentHp = GetComponentInParent<EnemyBasicBehavior>().enemyCurrentHealth;
			

			//Si le wolf n'est pas à portée d'attaque du joueur et qu'il peut bouger, il avance en direction du joueur.
			if (Vector2.Distance(transform.position, target.position) > startAttackRange && canMove == true)
			{
				wolfRb.velocity = direction * speed * Time.fixedDeltaTime;

				wolfAnimator.SetBool("isRunning", true);

				GetComponentInParent<EnemyBasicBehavior>().SetAnimDirection(direction);
				animDirection = GetComponentInParent<EnemyBasicBehavior>().animDirection;
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

			if (enemyCurrentHp <= 0)
			{
				Destroy(enemyPrefab);
			}
		}


		/// <summary>
		/// Wolf Attack Pattern
		/// </summary>
		/// <returns></returns>
		IEnumerator WolfAttack()
		{


			yield return new WaitForSeconds(timeBeforeTargetLock);

			dashTarget = target;
			dashDirection = new Vector2(dashTarget.position.x - transform.position.x, dashTarget.position.y - transform.position.y).normalized;
			GetComponentInParent<EnemyBasicBehavior>().SetAnimDirection(dashDirection);
			animDirection = GetComponentInParent<EnemyBasicBehavior>().animDirection;

			yield return new WaitForSeconds(timeBeforeWolfAttack);

			GetComponentInParent<CapsuleCollider2D>().isTrigger = true;
			GetComponent<CircleCollider2D>().isTrigger = true;
			isAttacking = true;

			wolfAnimator.SetFloat("Horizontal", animDirection.x);
			wolfAnimator.SetBool("isAttacking", true);

			float timer = 0.0f;

			while (timer < attackDuration)
			{
				wolfRb.velocity = dashDirection * attackSpeed * attackSpeedMofifier.Evaluate(timer / attackDuration);

				timer += Time.deltaTime;
				yield return null;
			}

			wolfRb.velocity = new Vector2(0, 0) * attackSpeed * Time.deltaTime;

			GetComponentInParent<CapsuleCollider2D>().isTrigger = false;
			GetComponent<CircleCollider2D>().isTrigger = false;
			isAttacking = false;

			wolfAnimator.SetBool("isAttacking", false);
			GetComponentInParent<EnemyBasicBehavior>().canMove = false;

			yield return new WaitForSeconds(recoilDuration);

			GetComponentInParent<EnemyBasicBehavior>().canMove = true;
			canMove = true;
			
		}

        private void OnTriggerEnter2D(Collider2D other)
		{
			if (isAttacking)
			{
				if (other.gameObject.tag == "Player")
				{
					other.gameObject.GetComponent<PlayerStats>().PlayerTakeDamage(enemyDamage);
				}
			}
			
		}

    }
}
