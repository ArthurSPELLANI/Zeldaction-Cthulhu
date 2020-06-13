﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FogScript : MonoBehaviour
{
    CapsuleCollider2D shadowColliBox;
    PlayerShadowMode playerShadowMode;
    public float timeBeforeShadowExit;


    private void OnEnable()
    {
        shadowColliBox = PlayerManager.Instance.playerShadowMode.GetComponentInChildren<CapsuleCollider2D>();
        playerShadowMode = PlayerManager.Instance.playerShadowMode;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == shadowColliBox)
        {
            PlayerManager.Instance.playerShadowMode.isInFog = true;
            StartCoroutine(GoBackHuman());
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col == shadowColliBox)
        {
            Debug.Log("yes");
            PlayerManager.Instance.playerShadowMode.isInFog = false;
            StopAllCoroutines();
        }
    }
    IEnumerator GoBackHuman()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        playerShadowMode.ShadowExit();
    }
}
