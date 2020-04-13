using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

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
		private int enemyDamage;
		private int enemyCurrentHp;

		public GameObject servantProjectile;

		private Rigidbody2D servantRb;
		public GameObject enemyPrefab;
		public GameObject enemyGraphics;

		public float startAttackRange;
		public float startRetreatRange;
		public float timeBeforeServantFire;

		public float recoilDuration;

		private bool canMove = true;

		void Awake()
		{
			servantRb = GetComponentInParent<Rigidbody2D>();
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

			//Si le servant n'est pas à portée d'attaque du joueur et qu'il peut bouger, il avance en direction du joueur.
			if (Vector2.Distance(transform.position, target.position) > startAttackRange && canMove == true)
			{
				servantRb.velocity = direction * speed * Time.fixedDeltaTime;
			}

			//Si le joueur est trop proche du servant, ce dernier s'en éloigne
			else if (Vector2.Distance(transform.position, target.position) < startRetreatRange && canMove == true)
			{
				servantRb.velocity = -direction * speed * Time.fixedDeltaTime;
			}

			//Si le joueur est à portée d'attaque du joueur et qu'il peut bouger, il arrête de bouger et lance son attaque.
			else if (Vector2.Distance(transform.position, target.position) < startAttackRange && Vector2.Distance(transform.position, target.position) > startRetreatRange && canMove == true)
			{
				canMove = false;
				servantRb.velocity = new Vector2(0, 0) * speed * Time.fixedDeltaTime;
				StartCoroutine(FireProjectile());
			}

			//Si l'ennemi n'a plus de pv, son entity est détruite.
			else if (enemyCurrentHp <= 0)
			{
				Destroy(enemyPrefab);
			}
		}


		/// <summary>
		/// The Servant fire a projectile after a short delay and then wait for another short delay.
		/// </summary>
		/// <returns></returns>
		IEnumerator FireProjectile()
		{
			enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.red;

			yield return new WaitForSeconds(timeBeforeServantFire);

			Instantiate(servantProjectile, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(recoilDuration);

			enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.white;
			canMove = true;
		}

	}
}
