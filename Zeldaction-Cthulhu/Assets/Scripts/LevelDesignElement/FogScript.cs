using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FogScript : MonoBehaviour
{
    BoxCollider2D shadowColliBox;
    PlayerShadowMode playerShadowMode;
    public float timeBeforeShadowExit;

    void Start()
    {
        shadowColliBox = GameObject.Find("Shadow").GetComponent<BoxCollider2D>();
        playerShadowMode = GameObject.Find("ShadowMode").GetComponent<PlayerShadowMode>();
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
