using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace Game
{
    public class LittleDoor : MonoBehaviour
    {
        SpriteRenderer render;
        bool destroyed = false;
        public GameObject coreParticle;

        public bool isLeft;
        public bool isRight;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (render.enabled == false && destroyed == false)
            {
                if (isLeft == true)
                {
                    destroyed = true;
                    BigDoorCave.Instance.isLeftDrestroyed = true;
                }

                if (isRight == true)
                {
                    destroyed = true;
                    BigDoorCave.Instance.isRightDestroyed = true;
                }

                Instantiate(coreParticle, transform.position, Quaternion.identity);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}

