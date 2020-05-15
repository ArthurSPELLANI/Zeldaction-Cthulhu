using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FogScript : MonoBehaviour
{
    CapsuleCollider2D shadowColliBox;
    PlayerShadowMode playerShadowMode;
    public float timeBeforeShadowExit;

    void Start()
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
            StartCoroutine(GoBackHuman());
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col == shadowColliBox)
        {
            Debug.Log("yes");
            StopAllCoroutines();
        }
    }
    IEnumerator GoBackHuman()
    {
        yield return new WaitForSecondsRealtime(timeBeforeShadowExit);
        playerShadowMode.ShadowExit();
    }
}
