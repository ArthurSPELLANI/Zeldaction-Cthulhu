using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public void Death()
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //desactivate behavior
            transform.parent.GetChild(2).gameObject.SetActive(false);

            GetComponentInParent<EnemyBasicBehavior>().enabled = false;
            GetComponentInParent<Collider2D>().enabled = false;

            //desactivate shadow catch graph
            transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
