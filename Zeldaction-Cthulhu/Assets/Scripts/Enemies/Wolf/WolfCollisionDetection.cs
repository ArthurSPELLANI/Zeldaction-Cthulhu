﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemy
{
	public class WolfCollisionDetection : MonoBehaviour
	{
		private int enemyDamage;

		void Awake()
		{
			enemyDamage = GetComponent<EnemyBasicBehavior>().enemyDamage;
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
