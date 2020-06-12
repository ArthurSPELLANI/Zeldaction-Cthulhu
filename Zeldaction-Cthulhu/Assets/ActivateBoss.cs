using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{

    public GameObject Boss;
    
    public void ActiveBoss()
    {
        Boss.SetActive(true);
    }
}
