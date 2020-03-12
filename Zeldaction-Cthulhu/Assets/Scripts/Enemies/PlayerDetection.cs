using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class PlayerDetection : MonoBehaviour
	{
        public GameObject Behavior;

		void Awake()
		{
			
		}

		void Start()
		{
			
		}

		void Update()
		{
			
		}

		// If the player is in the FieldOfView Collider, trigger Enemy's Behavior
		private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Behavior.SetActive(true);
            }
           
        }

    }
}