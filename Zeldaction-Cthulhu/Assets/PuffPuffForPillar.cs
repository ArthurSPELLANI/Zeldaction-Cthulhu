using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarSystem
{
    public class PuffPuffForPillar : MonoBehaviour
    {
        public GameObject PuffPuff;
        Pillar pillar;
        Rigidbody2D rb;

        float time;

        void Start()
        {
            pillar = GetComponent<Pillar>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (pillar.isMakingSound)
            {
                time += 0.2f * Time.deltaTime;

                if (time > 0.05f)
                {
                    time = 0;

                    if ((rb.velocity.x < -0.15f || rb.velocity.x > 0.15f))
                    {
                        Instantiate(PuffPuff, new Vector2(transform.position.x - 0.17f, transform.position.y - 0.425f), Quaternion.identity);
                        Instantiate(PuffPuff, new Vector2(transform.position.x, transform.position.y - 0.425f), Quaternion.identity);
                        Instantiate(PuffPuff, new Vector2(transform.position.x + 0.17f, transform.position.y - 0.425f), Quaternion.identity);
                    }
                    if ((rb.velocity.y < -0.15f || rb.velocity.y > 0.15f))
                        Instantiate(PuffPuff, new Vector2(transform.position.x, transform.position.y - 0.425f), Quaternion.identity);
                }
            }
        }


    }


}
