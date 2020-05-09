using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;


namespace Game
{
    public class BigDoorCave : Singleton<BigDoorCave>
    {
        public GameObject closedDoor;
        public GameObject openDoor;
        public GameObject cinematicDoor;
        public BoxCollider2D switchCollider;

        public bool isRightDestroyed;
        public bool isLeftDrestroyed;

        private void Awake()
        {
            MakeSingleton(false);
        }       

        // Update is called once per frame
        void Update()
        {
            if(isRightDestroyed == true && isLeftDrestroyed == true)
            {
                if (openDoor.activeSelf == false)
                {
                    openDoor.SetActive(true);
                    openDoor.SetActive(false);
                    cinematicDoor.SetActive(true);
                    switchCollider.enabled = true;
                }
            }
        }
    }
}



