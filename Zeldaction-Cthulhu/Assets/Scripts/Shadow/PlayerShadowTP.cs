using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

namespace Shadow
{
    public class PlayerShadowTP : MonoBehaviour
    {

        HashSet<GameObject> targetMarked;
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
            targetMarked = new HashSet<GameObject>();
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

            if(collision.gameObject.tag == "Shadow")
            {
                return;
            }

            if(shadMode.actionPoints > 0)
            {
                if (collision.gameObject.tag == "pillar" || collision.gameObject.tag == "NegativeFog")
                {
                    return;
                }                

                if (!targetMarked.Contains(collision.gameObject))
                {
                    targetMarked.Add(collision.gameObject);
                    if (collision.gameObject.tag is "Enemy")
                    {
                        collision.gameObject.GetComponent<EnemyBasicBehavior>().SanityDamage();
                        collision.gameObject.GetComponent<EnemyBasicBehavior>().MarkingCoolDown();
                    }
                }

            }              

        }

    }
}


