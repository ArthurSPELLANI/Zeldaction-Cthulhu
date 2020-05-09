using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FixNulBehaviourLightForest2 : MonoBehaviour
{

    void Start()
    {
        GameObject.Find("Behaviour_Player").SetActive(true);
        GameObject.Find("Attack").SetActive(true);
    }

   
}
