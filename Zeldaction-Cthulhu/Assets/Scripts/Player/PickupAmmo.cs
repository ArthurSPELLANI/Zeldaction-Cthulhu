﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PickupAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Shadow")
        {
            if (PlayerManager.Instance.playerShoot.ammunitions == 5)
            {
                PlayerManager.Instance.playerShoot.ammunitions += 1;
                Destroy(gameObject);
            }

            else if (PlayerManager.Instance.playerShoot.ammunitions < 6)
            {
                PlayerManager.Instance.playerShoot.ammunitions += 2;
                Destroy(gameObject);
            }            
        }
    }
}
