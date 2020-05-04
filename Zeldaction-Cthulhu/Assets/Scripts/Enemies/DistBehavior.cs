using System.Collections;
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
		public float maxChaseTime;

		private bool canMove = true;
		

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
			enemyDamage = GetComponentInParent<EnemyBasicBehavior>().enemyDamage;
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

				/*StopCoroutine(EndChase());
				forceAttack = false;
				canForce = true;*/
			}

			//Si le joueur est trop proche du servant, ce dernier s'en éloigne
			else if (Vector2.Distance(transform.position, target.position) < startRetreatRange && canMove == true)
			{
				servantRb.velocity = -direction * speed * Time.fixedDeltaTime;
				servantAnimator.SetBool("isRunning", true);

				/*if (canForce)
				{
					Debug.Log("alodazdazd");
					StartCoroutine(EndChase());
					canForce = false;
				}*/
				
			}

			//Si le joueur est à portée d'attaque du joueur et qu'il peut bouger, il arrête de bouger et lance son attaque.
			else if (Vector2.Distance(transform.position, target.position) < startAttackRange && Vector2.Distance(transform.position, target.position) > startRetreatRange && canMove == true /*|| forceAttack == true*/)
			{
				canMove = false;
				servantAnimator.SetBool("isAttacking", true);
				servantRb.velocity = new Vector2(0, 0) * speed * Time.fixedDeltaTime;
				StartCoroutine(FireProjectile());
				//son
				AudioManager.Instance.Play("attackRanged");

				/*StopCoroutine(EndChase());
				forceAttack = false;
				canForce = true;*/

			}

			//Si l'ennemi n'a plus de pv, son entity est détruite.
			if (enemyCurrentHp <= 0)
			{
				servantAnimator.SetBool("isDiying", true);
                if(GetComponentInParent<EnemyBasicBehavior>().isMarked == true)
                {
                    GetComponentInParent<EnemyBasicBehavior>().SanityReward();
                }

				//son
				AudioManager.Instance.Play("mortRanged");
				Destroy(enemyPrefab);
			}
		}


		/// <summary>
		/// The Servant fire a projectile after a short delay and then wait for another short delay.
		/// </summary>
		/// <returns></returns>
		IEnumerator FireProjectile()
		{
			//enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.red;

			yield return new WaitForSeconds(timeBeforeServantFire);

			Instantiate(servantProjectile, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(recoilDuration);

			///enemyGraphics.GetComponent<SpriteRenderer>().material.color = Color.white;
			canMove = true;
			servantAnimator.SetBool("isAttacking", false);
		}

		/*IEnumerator EndChase()
		{
			yield return new WaitForSeconds(maxChaseTime);

			forceAttack = true;
			canForce = true;
		}*/

	}
}
