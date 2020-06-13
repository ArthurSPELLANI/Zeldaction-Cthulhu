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
        public bool isLeftDestroyed;

        public int isLeftDestroyedInt;
        public int isRightDestroyedInt;

        private void Awake()
        {
            MakeSingleton(false);
        }

        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(isRightDestroyed == true && isLeftDestroyed == true)
            {
                if (openDoor.activeSelf == false)
                {
                    closedDoor.SetActive(false);
                    openDoor.SetActive(true);
                    cinematicDoor.SetActive(true);
                    switchCollider.enabled = true;
                }
            }
        }
    }
}



