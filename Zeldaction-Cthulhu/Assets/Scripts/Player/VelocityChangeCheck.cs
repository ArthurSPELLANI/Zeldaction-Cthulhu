using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player
{
    public class VelocityChangeCheck : MonoBehaviour
    {
        private float oldVelocity;
        private Rigidbody2D playerRb;

        public GameObject puffPuff;

        public Transform pufPufParentTransform;

        float time;
        
        

        // Start is called before the first frame update
        void Start()
        {
            playerRb = PlayerManager.Instance.GetComponent<Rigidbody2D>();
            oldVelocity = playerRb.velocity.magnitude;

        }

        // Update is called once per frame
        void Update()
        {
            {
                time += 0.5f * Time.deltaTime;

                if (time > 0.05f)
                {
                    time = 0;


                    if (oldVelocity != playerRb.velocity.magnitude)
                    {
                        //Spawn puff à la position dépendante de la vélocité du joueur

                        Instantiate(puffPuff, new Vector2(transform.position.x, transform.position.y - 0.25f), Quaternion.identity, pufPufParentTransform);

                    }

                    oldVelocity = playerRb.velocity.magnitude;
                }
            }
        }
    }
}

