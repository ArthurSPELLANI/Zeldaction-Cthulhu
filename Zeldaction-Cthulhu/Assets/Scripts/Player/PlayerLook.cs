using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        Vector3 aim;
        public GameObject lookObject;
        public float lookDistance;


        void Update()
        {
            if (PlayerManager.Instance.playerShoot.isAiming == false && PauseMenu.Instance.gameIsPaused == false)
            {
                aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical") * -1, 0.0f);

                if (PlayerManager.Instance.playerMovement.isWalking == false)
                {
                    if (aim.magnitude > 0.0f)
                    {
                        aim *= lookDistance;
                        lookObject.transform.localPosition = aim;
                    }
                }
                else
                {
                    lookObject.transform.localPosition = new Vector3(0, 0, 0);
                }
            }        
        }
    }
}
