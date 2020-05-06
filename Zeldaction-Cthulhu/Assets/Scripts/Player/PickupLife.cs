using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using AudioManaging;

public class PickupLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Shadow")
        {
            
            if (PlayerManager.Instance.playerStats.healNumber < PlayerManager.Instance.playerStats.maxHealNumber)
            {
                PlayerManager.Instance.playerStats.healNumber += 1;
                AudioManager.Instance.Play("priseHeal");
                Debug.Log("nombre de heal = " + PlayerManager.Instance.playerStats.healNumber);
                Destroy(gameObject);
            }

            /*if (PlayerManager.Instance.playerStats.playerCurrentHealth == 5)
            {
                PlayerManager.Instance.playerStats.playerCurrentHealth += 1;
                Destroy(gameObject);
            }

            else if (PlayerManager.Instance.playerStats.playerCurrentHealth < PlayerManager.Instance.playerStats.playerMaxHealth)
            {
                PlayerManager.Instance.playerStats.playerCurrentHealth += 2;
                Destroy(gameObject);
            }*/
        }
    }
}
