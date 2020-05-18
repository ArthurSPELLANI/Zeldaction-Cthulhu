using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class VelocityChangeCheck : MonoBehaviour
    {
        private float oldVelocity;
        private Rigidbody2D playerRb;

        public GameObject puffPuff;
        

        // Start is called before the first frame update
        void Start()
        {
            playerRb = PlayerManager.Instance.GetComponent<Rigidbody2D>();
            oldVelocity = playerRb.velocity.magnitude;

        }

        // Update is called once per frame
        void Update()
        {
            
            if(oldVelocity != playerRb.velocity.magnitude)
            {
                //Spawn puff à la position dépendante de la vélocité du joueur

                Instantiate(puffPuff, new Vector2(transform.position.x, transform.position.y - 0.25f), Quaternion.identity);
                
            }

            oldVelocity = playerRb.velocity.magnitude;
        }
    }
}

