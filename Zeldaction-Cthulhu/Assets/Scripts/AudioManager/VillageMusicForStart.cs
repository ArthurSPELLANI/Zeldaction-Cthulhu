using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class VillageMusicForStart : MonoBehaviour
{
    
    void Start()
    {
        PlayerManager.Instance.nearEnemyDetection.gameObject.SetActive(false);
    }

    
}
