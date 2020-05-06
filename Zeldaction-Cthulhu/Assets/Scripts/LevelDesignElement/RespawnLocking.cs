using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class RespawnLocking : MonoBehaviour
    {
        public bool respawnUnlocked = false;

        void OnTriggerEnter2D (Collider2D player)
        {
            if (player.gameObject.tag == "Player")
            {
                respawnUnlocked = true;
            }
        }
    }
}