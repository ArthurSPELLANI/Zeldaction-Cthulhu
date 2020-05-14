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

            if (transform.parent.GetChild(2).name == "ExploBehavior")
            {
                gameObject.SetActive(false);
            }

            transform.parent.GetChild(2).gameObject.SetActive(false);

            GetComponentInParent<EnemyBasicBehavior>().enabled = false;
            GetComponentInParent<Collider2D>().enabled = false;
        }
    }

}
