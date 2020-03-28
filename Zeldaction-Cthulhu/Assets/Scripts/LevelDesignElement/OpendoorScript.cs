using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpendoorScript : MonoBehaviour
{
    public GameObject Door;
    public GameObject Pillar;
    void Start()
    {
        
    }
    void Update()
    {
        if (Pillar.activeSelf) { Destroy(gameObject); Debug.Log("yes"); }
    }
}
