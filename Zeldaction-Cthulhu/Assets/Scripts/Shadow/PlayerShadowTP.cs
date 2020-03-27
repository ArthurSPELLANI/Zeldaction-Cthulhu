using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

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
                    shadMode.actionPoints -= 1;
                }

                shadMode.ShadowExit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if(shadMode.actionPoints > 0)
            {
                if (targetMarked.IndexOf(collision.gameObject) < 0)
                {
                    targetMarked.Add(collision.gameObject);
                    if (collision.gameObject.tag is "Enemy")
                    {
                        collision.gameObject.GetComponent<EnemyBasicBehavior>().SanityDamage();
                    }
                }
            }                      
            if (collision.tag != "pillar" || collision.tag != "NegativeFog")
                targetMarked.Add(collision.gameObject);
        }

    }
}


