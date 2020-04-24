using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarSystem
{
    public class CorruptionBeam : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed;
        Pillar originePillar;
        public LayerMask pillar;


        void Start()
        {
            originePillar = GetComponentInParent<Pillar>();
            rb.velocity = speed * originePillar.beamDir;
            StartCoroutine(Destruction());
        }

        IEnumerator Destruction()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D col)
        {            
                Destroy(gameObject);             
        }

    }
}


