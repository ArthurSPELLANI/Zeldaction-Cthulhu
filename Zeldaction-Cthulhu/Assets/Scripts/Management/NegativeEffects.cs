using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using AudioManaging;

public class NegativeEffects : MonoBehaviour
{

    public Light2D playerLight;
    public Light2D globalLight;
    public Volume volume;



    private void Update()
    {
        globalLight.intensity = PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity;
        playerLight.intensity = 1 - globalLight.intensity;

        if(PlayerManager.Instance.playerShadowMode.maxSanity >= 75)
        {
            if(PlayerManager.Instance.playerShadowMode.sanity < 20)
            {
                volume.enabled = true;
            }
            else
            {
                volume.enabled = false;
            }
        }

        //pour le son
        if (PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity < 0.2f)
        {
            AudioManager.Instance.negativeEffectPalier = 3;
        }
        if (PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity < 0.4f)
        {
            AudioManager.Instance.negativeEffectPalier = 2;
        }
        else if (PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity < 0.7f)
        {
            AudioManager.Instance.negativeEffectPalier = 1;
        }

    }


}
