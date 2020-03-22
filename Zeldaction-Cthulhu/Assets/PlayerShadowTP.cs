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
            targetMarked = new List<GameObject>();
            shadMode = gameObject.GetComponentInParent<PlayerShadowMode>();
        }
        
        void Update()
        {
            if (Input.GetButtonDown("TP"))
            {
                Debug.Log("TP");
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


