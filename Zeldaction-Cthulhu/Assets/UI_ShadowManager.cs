using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class UI_ShadowManager : MonoBehaviour
{
    Animator anim;
    PlayerShadowMode shadowMode;
    SpriteRenderer render;

    private void Start()
    {
        shadowMode = PlayerManager.Instance.playerShadowMode.GetComponent<PlayerShadowMode>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(shadowMode.isShadowActivated == true)
        {
            anim.enabled = true;
        }
        else if (anim.enabled == true)
        {
            anim.SetBool("isOut", true);
        }


    }

    public void AnimOut()
    {
        anim.enabled = false;
        anim.SetBool("isOut", false);
        render.sprite = null;     
     
    }

}
