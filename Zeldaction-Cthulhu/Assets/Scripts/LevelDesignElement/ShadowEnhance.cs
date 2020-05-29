using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ShadowEnhance : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButtonDown("Interract"))
        {
            if (collision.gameObject.tag == "Player" && PlayerManager.Instance.playerShadowMode.fragment >=3)
            {
                PlayerManager.Instance.playerShadowMode.ShadowEnhance();             
            }
        }
    }
}
