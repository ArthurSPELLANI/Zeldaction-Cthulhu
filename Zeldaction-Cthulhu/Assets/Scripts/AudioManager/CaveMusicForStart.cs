using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class CaveMusicForStart : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.Instance.nearEnemyDetection.gameObject.SetActive(false);
    }
}
