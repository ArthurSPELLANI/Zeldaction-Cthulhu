using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
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


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Behavior.SetActive(true);
            }
           
        }

    }
}