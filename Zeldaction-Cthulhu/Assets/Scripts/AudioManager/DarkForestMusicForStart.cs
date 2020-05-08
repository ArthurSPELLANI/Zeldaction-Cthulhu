using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DarkForestMusicForStart : MonoBehaviour
{
    
    void Start()
    {
        PlayerManager.Instance.nearEnemyDetection.gameObject.SetActive(true);
        PlayerManager.Instance.nearEnemyDetection.Music = this.gameObject;
    }

}
