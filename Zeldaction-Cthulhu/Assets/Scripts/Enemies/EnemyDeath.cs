using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public void Death()
        {
            Destroy(transform.parent.GetChild(2).gameObject);
            GetComponentInParent<EnemyBasicBehavior>().enabled = false;
            GetComponentInParent<Collider2D>().enabled = false;
        }
    }

}
