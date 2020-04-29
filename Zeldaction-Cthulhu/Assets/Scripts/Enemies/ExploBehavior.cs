using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Boss;


namespace Enemy
{
	public class ExploBehavior : MonoBehaviour
	{
		private Rigidbody2D exploRb;
		private GameObject player;
		private Transform target;
		private int speed;
		private int enemyDamage;
		public GameObject enemyPrefab;
		private Vector2 direction;
		private bool canMove = true;
		private bool canExplode = true;
		private int maxHp;
		private int currentHp;
		public float explosionTime;
		[Range(0, 1)]
		public float explosionRange;
		public LayerMask playerLayer;
		public LayerMask enemyLayer;

		public Animator exploAnimator;
		private Vector2 animDirection;

		void Awake()
		{
			exploRb = GetComponentInParent<Rigidbody2D>();
			player = GameObject.Find("Player");
			target = player.transform;
			speed = GetComponentInParent<EnemyBasicBehavior>().speed;
			enemyDamage = GetComponentInParent<EnemyBasicBehavior>().enemyDamage;
			maxHp = GetComponentInParent<EnemyBasicBehavior>().enemyMaxHealth;	
		}

		void Start()
		{

		}

		void Update()
		{
			direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
			currentHp = GetComponentInParent<EnemyBasicBehavior>().enemyCurrentHealth;

			GetComponentInParent<EnemyBasicBehavior>().SetAnimDirection(direction);
			animDirection = GetComponentInParent<EnemyBasicBehavior>().animDirection;
			exploAnimator.SetFloat("Horizontal", animDirection.x);

			if (canMove == true)
			{
				exploRb.velocity = direction * speed * Time.fixedDeltaTime;
			}

			if (currentHp < maxHp && canExplode == true)
			{
				canMove = false;
				canExplode = false;
				exploRb.velocity = new Vector2(0, 0) * speed * Time.fixedDeltaTime;
				StartCoroutine(TimeBeforeExplo());
			}

		}

		IEnumerator TimeBeforeExplo()
		{
			exploAnimator.SetBool("isExploding", true);

			Debug.Log("it's gonna be bim boom");
			if (currentHp > 0)
			{
				yield return new WaitForSeconds(explosionTime);
				Explosion();
			}
			else
			{
				Explosion();
			}
		}


		private void Explosion()
		{

			Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, explosionRange, playerLayer);
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayer);
			

			foreach (Collider2D player in hitPlayer)
			{
				player.GetComponent<PlayerStats>().PlayerTakeDamage(enemyDamage);
			}

			foreach (Collider2D enemy in hitEnemies)
			{
				if (enemy.CompareTag("Enemy"))
				{
					enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(enemyDamage, transform.position, 100);
				}

				else if (enemy.CompareTag("Boss"))
				{
					enemy.transform.parent.GetComponentInChildren<Phase2Pattern1>().ExploHitBoss();
				}
				
			}

			Destroy(enemyPrefab);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, explosionRange);
		}

		public void StopScratching()
		{
			exploAnimator.SetBool("isScratching", false);
		}
	}
}
