using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class LightSource : MonoBehaviour
{
    PlayerShadowMode shadMode;
    public float sanityGainLight;
    public Collider2D range;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerManager.Instance.playerShadowMode.isShadowActivated == false)
        {
            if(PlayerManager.Instance.playerShadowMode.sanity < PlayerManager.Instance.playerShadowMode.maxSanity)
            {
                PlayerManager.Instance.playerShadowMode.sanity += Time.deltaTime * sanityGainLight;
            }            
        }
    }
}

