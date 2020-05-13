using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Game;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;

public class LightSource : MonoBehaviour
{
    PlayerShadowMode shadMode;
    public float sanityGainLight;
    public Collider2D range;
    public Light2D[] lightsource;
    Light2D globalLight;

    private void Start()
    {
        globalLight = GameManager.Instance.globalLight;
    }

    private void Update()
    {
        for (int i = 0; i < lightsource.Length; i++)
        {
            lightsource[i].intensity = 1 - globalLight.intensity;
        }        
    }

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

