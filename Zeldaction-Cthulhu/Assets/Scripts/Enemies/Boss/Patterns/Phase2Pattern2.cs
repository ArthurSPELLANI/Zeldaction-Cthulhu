﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Player;
using PillarSystem;
using AudioManaging;


namespace Boss
{
    public class Phase2Pattern2 : MonoBehaviour
    {

        private GameObject player;
        private Rigidbody2D playerRb;
        private GameObject graphics;
        private GameObject pillarsParent;
        private GameObject pillarInt;
        private GameObject pillarOut;
        private GameObject pullPoint;

        public GameObject laser;
        public GameObject vortex;

        public float pullForce;
        public float laserCountdown;
        public int laserDmg;
        public float laserThickness;
        public float laserDistance;

        public float timeBeforeWeakStatusBegin;
        public float timeBeforeWeakStatusEnd;
        public float timeBeforePatternStart;
        public float timeBeforePatternEnd;

        public LayerMask playerLayer;

        private bool pillarHaveSpawn;

        public Animator animator;

        void Awake()
        {
            player = GameObject.Find("Player");
            playerRb = player.GetComponent<Rigidbody2D>();
            graphics = transform.parent.GetChild(0).gameObject;
            pillarsParent = transform.GetChild(3).gameObject;
            pillarInt = pillarsParent.transform.GetChild(0).gameObject;
            pullPoint = transform.GetChild(5).gameObject;
        }

        void OnEnable()
        {
            pillarHaveSpawn = false;
            StartCoroutine(PatternStart());
        }

        void Update()
        {
            if (pillarHaveSpawn)
            {
                if (pillarOut.transform.GetChild(0).gameObject.activeSelf)
                {
                    vortex.SetActive(false);
                }
                else
                {
                    vortex.SetActive(true);
                }
            }
        }

        private IEnumerator PatternStart()
        {
            yield return new WaitForSeconds(timeBeforePatternStart);

            StartCoroutine(PullPlayer());
        }

        //Add force to the avatar so he goes at the center of the arena 
        private IEnumerator PullPlayer()
        {
            while (player.transform.position != pullPoint.transform.position)
            {
                playerRb.AddForce((pullPoint.transform.position - player.transform.position) * pullForce);

                //safe zone around the pullPoint
                Collider2D hitPlayer = Physics2D.OverlapCircle(pullPoint.transform.position, 0.1f, playerLayer);

                if(hitPlayer != null)
                {
                    break;
                }

                yield return null;

            }

            //walls.SetActive(true);

            SpawnPillars();
        }

        private void SpawnPillars()
        {
            int i = Random.Range(1, 5);

            pillarOut = pillarsParent.transform.GetChild(i).gameObject;

            pillarInt.SetActive(true);
            pillarInt.GetComponent<Pillar>().isCharged = true;

            pillarOut.SetActive(true);
            pillarOut.GetComponent<Pillar>().isCharged = false;

            pillarHaveSpawn = true;
            StartCoroutine(Laser());
        }

        private IEnumerator Laser()
        {
            //graphics.GetComponent<SpriteRenderer>().color = Color.green;
            animator.SetBool("isChargingLaser", true);


            yield return new WaitForSeconds(laserCountdown);

            //graphics.GetComponent<SpriteRenderer>().color = Color.red;
            laser.SetActive(true);
            AudioManager.Instance.Play("Laser");

            yield return new WaitForSeconds(1f);

            vortex.SetActive(false);
            pillarHaveSpawn = false;
            pillarInt.GetComponent<Pillar>().isCharged = true;
            pillarInt.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            pillarInt.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            pillarOut.GetComponent<Pillar>().isCharged = false;
            pillarOut.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            pillarOut.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            pillarOut.SetActive(false);
            pillarInt.SetActive(false);

            RaycastHit2D[] hitPlayer = Physics2D.CircleCastAll(transform.position, laserThickness, new Vector2(0, -1), laserDistance, playerLayer);

            if (hitPlayer.Length == 0)
            {
                Debug.Log("laser didn't hit the player");

                yield return new WaitForSeconds(timeBeforeWeakStatusBegin);

                animator.SetBool("isWeak", true);
                animator.SetBool("isChargingLaser", false);
                laser.SetActive(false);
                transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

                yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

                animator.SetBool("isWeak", false);
                transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;

                yield return new WaitForSeconds(timeBeforePatternEnd);

                GetComponent<Phase2PatternManager>().NextPatternSelection();
            }
            else
            {
                Debug.Log("laser hit the player");
                foreach (RaycastHit2D player in hitPlayer)
                {
                    player.collider.GetComponent<PlayerStats>().PlayerTakeDamage(laserDmg);  
                }               
                
                yield return new WaitForSeconds(timeBeforePatternEnd);
                //graphics.GetComponent<SpriteRenderer>().color = Color.white;
                animator.SetBool("isChargingLaser", false);
                laser.SetActive(false);

                GetComponent<Phase2PatternManager>().NextPatternSelection();
            }

            

        }

        /*private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 1, 1), laserThickness);
            UnityEditor.Handles.DrawWireDisc(new Vector3(transform.position.x, transform.position.y - laserDistance, transform.position.z), new Vector3(0, 1, 1), laserThickness);
            UnityEditor.Handles.DrawWireDisc(new Vector3(transform.position.x, transform.position.y - laserDistance/2, transform.position.z), new Vector3(0, 1, 1), laserThickness);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, new Vector2(0, -laserDistance));
        }*/
    }
}

