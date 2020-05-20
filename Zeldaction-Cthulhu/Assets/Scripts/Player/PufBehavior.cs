using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufBehavior : MonoBehaviour
{

    public float destroyPuf;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyPuf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
