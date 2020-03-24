using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


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


		void Awake()
		{
			exploRb = GetComponentInParent<Rigidbody2D>();
			player = GameObject.Find("Player");
			target = player.transform;
			speed = enemyPrefab.GetComponent<EnemyBasicBehavior>().speed;
			enemyDamage = enemyPrefab.GetComponent<EnemyBasicBehavior>().enemyDamage;
			maxHp = enemyPrefab.GetComponent<EnemyBasicBehavior>().enemyMaxHealth;	
		}

		void Start()
		{

		}

		void Update()
		{
			direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
			currentHp = enemyPrefab.GetComponent<EnemyBasicBehavior>().enemyCurrentHealth;

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
				enemy.GetComponentInChildren<EnemyBasicBehavior>().TakeDamage(enemyDamage);
			}

			Destroy(enemyPrefab);
		}


	}
}
