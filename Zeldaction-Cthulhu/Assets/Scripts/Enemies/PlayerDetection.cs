using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class PlayerDetection : MonoBehaviour
	{
		[HideInInspector] public bool isDetected = false;
        public GameObject behavior;
		private Rigidbody2D enemyRb;
		public float alertRange;
		public LayerMask enemyLayer;

		void Awake()
		{
			enemyRb = GetComponentInParent<Rigidbody2D>();
		}

		void Start()
		{
			
		}

		void Update()
		{
			//Detect in which direction the enemy is going and orientate the field of view in this same direction.
			if (isDetected == false)
			{
				if (enemyRb.velocity.x >= 0.6)
				{
					transform.eulerAngles = new Vector3(0, 0, 270);
				}
				else if (enemyRb.velocity.x <= -0.6)
				{
					transform.eulerAngles = new Vector3(0, 0, 90);
				}
				else if (enemyRb.velocity.y >= 0.6)
				{
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
				else if (enemyRb.velocity.y <= -0.6)
				{
					transform.eulerAngles = new Vector3(0, 0, 180);
				}
			}
			else
			{
				behavior.SetActive(true);
				GetComponent<PolygonCollider2D>().enabled = false;
			}
	
		}

		// If the player is in the FieldOfView Collider, trigger Enemy's Behavior
		private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {

				isDetected = true;

				Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, alertRange, enemyLayer);

				foreach (Collider2D enemy in hitEnemies)
				{
					enemy.GetComponentInChildren<PlayerDetection>().isDetected = true;
				}
			}
           
        }

    }
}