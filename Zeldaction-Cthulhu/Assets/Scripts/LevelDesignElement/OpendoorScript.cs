using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpendoorScript : MonoBehaviour
{
    public GameObject Pillar;
    private float ChildNbr;
    void Start()
    {
        ChildNbr = transform.childCount;
    }

    void Update()
    {
        if (Pillar.activeSelf) 
        {
            GetComponent<Collider2D>().enabled = false;

            for (int i = 0; i < ChildNbr; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;

            for (int i = 0; i < ChildNbr; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
