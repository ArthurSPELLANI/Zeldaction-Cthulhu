using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class AnimFireProjectile : MonoBehaviour
    {
        private GameObject servantBehaviorGo;

        private void Awake()
        {
            servantBehaviorGo = transform.parent.GetChild(2).gameObject;
        }

        public void FireProjectile()
        {
            StartCoroutine(servantBehaviorGo.GetComponent<DistBehavior>().FireProjectile());
        }

        //nulachier
        /*public void GoToNextAttack()
        {
            servantGraphicsGo.GetComponent<DistBehavior>().canMove = true;
            servantGraphicsGo.GetComponent<DistBehavior>().servantAnimator.SetBool("isAttacking", false);
        }*/

    }
}

