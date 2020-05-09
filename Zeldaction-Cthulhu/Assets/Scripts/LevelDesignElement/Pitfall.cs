using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace LevelDesign
{
    public class Pitfall : MonoBehaviour
    {
        public GameObject player;
        public GameObject respawnList;
        private int respawnNbr;
        private float compareDist;
        private float distance;
        public Transform[] respawn;
        public Transform trueRespawn;
        //public Transform respawn1;
        //public Transform respawn2;
        public Rigidbody2D rb;
        public bool onPlatform;      
        //private float sqrOffset1;
        //private float sqrOffset2;

        void Awake()
        {
            respawnNbr = respawnList.transform.childCount;
        }

        void Start()
        {
            RespawnList();
            player = PlayerManager.Instance.gameObject;
        }

        void Update()
        {
            /*Vector2 offset1 = respawn1.position - player.transform.position;
            sqrOffset1 = offset1.sqrMagnitude;
            Vector2 offset2 = respawn2.position - player.transform.position;
            sqrOffset2 = offset2.sqrMagnitude;*/

            /*if (Input.GetKey(KeyCode.Space))
            {
                RaycastHit2D boxResult;
                boxResult = Physics2D.BoxCast(gameObject.transform.position, new Vector2(1, 0.5f), 0f, new Vector2(0, -1), 0, 25f);
                if (boxResult.collider == respawn1.collider)
                {
                    Debug.Log("Respawn 1 !");
                }
                if (boxResult.collider == respawn2.collider)
                {
                    Debug.Log("Respawn 2 !");
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);
                 RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 10f);

                 if (hit)
                 {
                     Debug.Log("Hit something : " + hit.collider.name);
                     hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                 }
            }*/
        }

        private void RespawnList()
        {
            respawn = new Transform[respawnNbr];

            for (int i = 0; i < respawnNbr; i++)
            {
                respawn[i] = respawnList.transform.GetChild(i);
            }
        }

        private void SelectingRespawn()
        {
            compareDist = 10000f;

            for (int i = 0; i < respawnNbr; i++)
            {
                distance = (player.transform.position - respawn[i].transform.position).magnitude;
                if (distance < compareDist && respawn[i].GetComponent<RespawnLocking>().respawnUnlocked)
                {
                    compareDist = distance;
                    trueRespawn.transform.position = respawn[i].transform.position;
                    //Debug.Log(respawn[i]);
                }
            }
        }

        void OnTriggerStay2D(Collider2D player)
        {
            if (player.gameObject.tag == "Player" && !onPlatform)
            {
                StartCoroutine(PitfallActivation());
                //Debug.Log("Fall");      
            }
        }

        IEnumerator PitfallActivation()
        {
            SelectingRespawn();
            yield return new WaitForSeconds(0.5f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(0.5f);
            player.transform.position = trueRespawn.position;
            /*if (sqrOffset1 < sqrOffset2)
            {
                trueRespawn.position = respawn1.position;
                player.transform.position = trueRespawn.position;
            }
            if (sqrOffset1 > sqrOffset2)
            {
                trueRespawn.position = respawn2.position;
                player.transform.position = trueRespawn.position;
            }*/
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}


