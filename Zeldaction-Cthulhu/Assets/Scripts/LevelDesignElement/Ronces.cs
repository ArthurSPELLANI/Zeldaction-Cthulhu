using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ronces : MonoBehaviour
{
    public GameObject particle;
    Quaternion rotation = Quaternion.identity;

    public void Destroy()
    {
        Instantiate(particle, transform.position, rotation);
        Destroy(gameObject);
    }
}


