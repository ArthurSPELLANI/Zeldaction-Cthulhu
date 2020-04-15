using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Platform : MonoBehaviour
    {
        public GameObject player;
        public GameObject pitfall;

        void OnTriggerEnter2D(Collider2D player)
        {
            if (player.gameObject.tag == "Player")
            {
                pitfall.GetComponent<Pitfall>().onPlatform = true;
            }
        }
        void OnTriggerExit2D(Collider2D player)
        {
            if (player.gameObject.tag == "Player")
            {
                pitfall.GetComponent<Pitfall>().onPlatform = false;
            }
        }
    }
}