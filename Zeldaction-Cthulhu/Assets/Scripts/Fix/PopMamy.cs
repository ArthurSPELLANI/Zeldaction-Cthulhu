using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMamy : MonoBehaviour
{
    public GameObject mamyPNJ;
    public GameObject mamyBoss;
    

    
    void Start()
    {
        mamyBoss.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mamyBoss.gameObject.SetActive(true);
            mamyPNJ.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
