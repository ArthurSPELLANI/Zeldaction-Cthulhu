using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class PlayerDetection : MonoBehaviour
	{
		[HideInInspector] public bool isDetected = false;
        public GameObject Behavior;
		private Rigidbody2D EnemyRb;

		void Awake()
		{
			EnemyRb = GetComponentInParent<Rigidbody2D>();
		}

		void Start()
		{
			
		}

		void Update()
		{
			//Detect in which direction the enemy is going and orientate the field of view in this same direction.
			if (isDetected == false)
			{
				if (EnemyRb.velocity.x >= 1)
				{
					transform.eulerAngles = new Vector3(0, 0, 270);
				}
				else if (EnemyRb.velocity.x <= -1)
				{
					transform.eulerAngles = new Vector3(0, 0, 90);
				}
				else if (EnemyRb.velocity.y >= 1)
				{
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
				else if (EnemyRb.velocity.y <= -1)
				{
					transform.eulerAngles = new Vector3(0, 0, 180);
				}
			}
	
		}

		// If the player is in the FieldOfView Collider, trigger Enemy's Behavior
		private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Behavior.SetActive(true);
				isDetected = true;
				GetComponent<PolygonCollider2D>().enabled = false;
            }
           
        }

    }
}