using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace Game
{
    public class LittleDoor : MonoBehaviour
    {
        SpriteRenderer render;
        public bool destroyed = false;
        public int destroyedInt;
        public GameObject coreParticle;

        public bool isLeft;
        public bool isRight;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();

            if(destroyedInt == 1)
            {
                destroyed = true;
            }
            else if(destroyedInt == 0)
            {
                destroyed = false;
            }
        }

        void Update()
        {
            if (render.enabled == false && destroyed == false)
            {
                if (isLeft == true)
                {
                    destroyed = true;
                    destroyedInt = 1;
                    BigDoorCave.Instance.isLeftDestroyed = true;
                }

                if (isRight == true)
                {
                    destroyed = true;
                    destroyedInt = 1;
                    BigDoorCave.Instance.isRightDestroyed = true;
                }

                Instantiate(coreParticle, transform.position, Quaternion.identity);
                transform.GetChild(0).gameObject.SetActive(false);

                
            }

            if (destroyed == true)
            {
                if (render.enabled == true)
                {
                    render.enabled = false;

                    if (isLeft)
                    {
                        BigDoorCave.Instance.isLeftDestroyed = true;
                    }
                    if (isRight)
                    {
                        BigDoorCave.Instance.isRightDestroyed = true;
                    }
                }
            }
        }



    }
}

