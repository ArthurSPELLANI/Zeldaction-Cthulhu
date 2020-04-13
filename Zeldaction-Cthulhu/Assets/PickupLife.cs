using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PickupLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Shadow")
        {
            if (PlayerManager.Instance.playerStats.playerCurrentHealth == PlayerManager.Instance.playerStats.playerMaxHealth -1)
            {
                PlayerManager.Instance.playerStats.playerCurrentHealth += 1;
                Destroy(gameObject);
            }

            if (PlayerManager.Instance.playerStats.playerCurrentHealth < PlayerManager.Instance.playerStats.playerMaxHealth)
            {
                PlayerManager.Instance.playerStats.playerCurrentHealth += 2;
                Destroy(gameObject);
            }
        }
    }
}
