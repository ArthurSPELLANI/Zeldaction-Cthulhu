using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManaging;

namespace Player
{
    public class NearEnemyDetection : MonoBehaviour
    {
        public LayerMask EnemyLayer;
        public GameObject Music;
        public MusicManager musicManager;
        public CircleCollider2D Detection;

        void Start()
        {
            
            //Detection = GetComponent<CircleCollider2D>();
        }

        void Update()
        {
            if (musicManager == null)
                musicManager = Music.GetComponent<MusicManager>();

            if (musicManager != null)
            {
                if (Detection.IsTouchingLayers(EnemyLayer))
                    musicManager.isOnFight = true;

                if (!Detection.IsTouchingLayers(EnemyLayer))
                    musicManager.isOnFight = false;
            }
            

        }
    }
}

