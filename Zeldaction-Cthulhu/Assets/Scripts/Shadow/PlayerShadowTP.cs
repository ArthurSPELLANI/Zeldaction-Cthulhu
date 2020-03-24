using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Shadow
{
    public class PlayerShadowTP : MonoBehaviour
    {

        public List<GameObject> targetMarked;
        PlayerShadowMode shadMode;

        void Awake()
        {
            
        }

        void Start()
        {            
            shadMode = gameObject.GetComponentInParent<PlayerShadowMode>();
        }

        private void OnEnable()
        {
            targetMarked = new List<GameObject>();
        }

        void Update()
        {
            if (Input.GetButtonDown("TP"))
            {
                foreach  (GameObject target in targetMarked)
                {
                    target.transform.position = this.gameObject.transform.position;                    
                }

                shadMode.ShadowExit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            targetMarked.Add(collision.gameObject);
        }
    }
}


