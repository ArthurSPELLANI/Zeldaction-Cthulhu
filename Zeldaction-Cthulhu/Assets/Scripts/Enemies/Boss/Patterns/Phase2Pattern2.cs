using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    public class Phase2Pattern2 : MonoBehaviour
    {

        private GameObject player;
        private Rigidbody2D playerRb;
        private GameObject walls;

        void Awake()
        {
            player = GameObject.Find("Player");
            playerRb = player.GetComponent<Rigidbody2D>();
            walls = transform.GetChild(2).gameObject;
            
        }

        void OnEnable()
        {
            //pull player first

            //then spawn pillars

            //finally laser 
        }

        void Update()
        {

        }

        private void PullPlayer()
        {
            //Pull player on center of wall with rigidbody

            //activate wall game object to constaint player

            //Start charging laser state
        }

        private void SpawnPillars()
        {
            //random sur la selection du pillars exterieur en off

            // spawn fix du pillier à l'interieur 
        }

        private IEnumerator Laser()
        {
            //Prepare for laser visu

            yield return new WaitForSeconds(0f);

            //Ray cast to detect if the player is there (change ray thickness so it is the proper size

            //if the player get it trigger player take dmg with a dmg value

            //if the player manage to escape in time just enter weak state for multiple seconds

            //Go to next pattern.

            
        }

    }
}

