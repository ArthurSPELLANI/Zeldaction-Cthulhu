using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.Playables;

public class StartCineOenDoor : MonoBehaviour
{

    public GameObject Door1;
    public GameObject Door2;
    public GameObject Cine;

    LittleDoor d1;
    LittleDoor d2;


    void Start()
    {
        d1 = Door1.GetComponent<LittleDoor>();
        d2 = Door2.GetComponent<LittleDoor>();
    }
    void Update()
    {
        if (d1.destroyed && d2.destroyed)
        {
            Cine.SetActive(true);
        }

    }
}
