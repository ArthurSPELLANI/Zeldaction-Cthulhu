using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Pitfall : MonoBehaviour
    {
        public GameObject player;
        public Transform trueRespawn;
        public Transform respawn1;
        public Transform respawn2;
        public Rigidbody2D rb;
        public bool onPlatform;
        private float sqrOffset1;
        private float sqrOffset2;

        void Update()
        {
            Vector2 offset1 = respawn1.position - player.transform.position;
            sqrOffset1 = offset1.sqrMagnitude;
            Vector2 offset2 = respawn2.position - player.transform.position;
            sqrOffset2 = offset2.sqrMagnitude;
        }

        void OnTriggerStay2D(Collider2D player)
        {
            if (player.gameObject.tag == "Player" && !onPlatform)
            {
                StartCoroutine(PitfallActivation());
                Debug.Log("Fall");      
            }
        }

        IEnumerator PitfallActivation()
        {
            yield return new WaitForSeconds(0.5f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(0.5f);        
            if (sqrOffset1 < sqrOffset2)
            {
                trueRespawn.position = respawn1.position;
                player.transform.position = trueRespawn.position;
            }
            if (sqrOffset1 > sqrOffset2)
            {
                trueRespawn.position = respawn2.position;
                player.transform.position = trueRespawn.position;
            }
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}


