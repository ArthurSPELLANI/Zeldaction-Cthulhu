using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpendoorScript : MonoBehaviour
{
    public GameObject Pillar;
    void Start()
    {
        
    }
    void Update()
    {
        if (Pillar.activeSelf) 
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
