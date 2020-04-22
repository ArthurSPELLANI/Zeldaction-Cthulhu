using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using PillarSystem;


namespace Boss
{
    public class Phase2Pattern2 : MonoBehaviour
    {

        private GameObject player;
        private Rigidbody2D playerRb;
        private GameObject walls;
        private GameObject graphics;
        private GameObject pillarsParent;
        private GameObject pillarInt;
        private GameObject pillarOut;


        public float laserCountdown;
        public int laserDmg;
        public float laserThickness;
        public float laserDistance;

        public float timeBeforeWeakStatusBegin;
        public float timeBeforeWeakStatusEnd;
        public float timeBeforePatternStart;
        public float timeBeforePatternEnd;

        public LayerMask playerLayer;

        void Awake()
        {
            player = GameObject.Find("Player");
            playerRb = player.GetComponent<Rigidbody2D>();
            walls = transform.GetChild(2).gameObject;
            graphics = transform.parent.GetChild(0).gameObject;
            pillarsParent = transform.GetChild(3).gameObject;
            pillarInt = pillarsParent.transform.GetChild(0).gameObject;

        }

        void OnEnable()
        {
            //pull player first

            //then spawn pillars


            StartCoroutine(PatternStart());
        }

        void Update()
        {

        }

        private IEnumerator PatternStart()
        {
            yield return new WaitForSeconds(timeBeforePatternStart);

            SpawnPillars();
        }


        private void PullPlayer()
        {
            //Pull player on center of wall with rigidbody

            //activate wall game object to constaint player

            walls.SetActive(true);
            SpawnPillars();
        }

        private void SpawnPillars()
        {
            //int i = Random.Range(1, 5);
            int i = 2;

            pillarOut = pillarsParent.transform.GetChild(i).gameObject;

            pillarInt.SetActive(true);
            pillarInt.GetComponent<Pillar>().isCharged = true;

            pillarOut.SetActive(true);
            pillarOut.GetComponent<Pillar>().isCharged = false;

            StartCoroutine(Laser());
        }

        private IEnumerator Laser()
        {
            graphics.GetComponent<SpriteRenderer>().color = Color.green;

            yield return new WaitForSeconds(laserCountdown);

            graphics.GetComponent<SpriteRenderer>().color = Color.red;

            RaycastHit2D[] hitPlayer = Physics2D.CircleCastAll(transform.position, laserThickness, new Vector2(0, -1), laserDistance, playerLayer);
            Debug.Log(hitPlayer[0].collider);

            if (hitPlayer == null)
            {
                StartCoroutine(weakStatus());

                yield return new WaitForSeconds(timeBeforePatternEnd);

                GetComponent<Phase2PatternManager>().NextPatternSelection();
            }
            else
            {
                foreach (RaycastHit2D player in hitPlayer)
                {

                    player.collider.GetComponent<PlayerStats>().PlayerTakeDamage(laserDmg);  
                }

                walls.SetActive(false);

                yield return new WaitForSeconds(timeBeforePatternEnd);
                graphics.GetComponent<SpriteRenderer>().color = Color.white;

                GetComponent<Phase2PatternManager>().NextPatternSelection();
            }

        }

        private IEnumerator weakStatus()
        {
            yield return new WaitForSeconds(timeBeforeWeakStatusBegin);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

            yield return new WaitForSeconds(timeBeforeWeakStatusEnd);

            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, new Vector3(0, -1, 0));
        }


        //Physics2D.CircleCastAll(transform.position, laserThickness, new Vector2(0, -1), playerLayer);
    }
}

