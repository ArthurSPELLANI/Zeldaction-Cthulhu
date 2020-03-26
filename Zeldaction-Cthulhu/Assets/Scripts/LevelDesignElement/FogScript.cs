using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FogScript : MonoBehaviour
{
    PlayerShadowMode playerShadowMode;
    public float timeBeforeShadowExit;

    void Start()
    {
        playerShadowMode = GameObject.Find("ShadowMode").GetComponent<PlayerShadowMode>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Shadow")
        {
            Debug.Log("yes");
            StartCoroutine(GoBackHuman());
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Shadow")
        {
            StopCoroutine(GoBackHuman());
        }
    }
    IEnumerator GoBackHuman()
    {
        Debug.Log("yes");

        yield return new WaitForSeconds(timeBeforeShadowExit);
        playerShadowMode.ShadowExit();
    }
}
