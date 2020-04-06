using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Boss
{
    public class BossCollisionDetection : MonoBehaviour
    {
        private int currentPattern;
        public GameObject phase1Behavior;
        private int damage;


        void Awake()
        {
            damage = phase1Behavior.GetComponent<Phase1Pattern2>().dmg;
        }

        void Update()
        {
            currentPattern = phase1Behavior.GetComponent<Phase1PatternManager>().patternNbr;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (currentPattern == 2)
            {
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
                }

                if (other.CompareTag("Enviro"))
                {
                    phase1Behavior.GetComponent<Phase1Pattern2>().hasHitWall = true;
                }
            }
        }

    }
}
