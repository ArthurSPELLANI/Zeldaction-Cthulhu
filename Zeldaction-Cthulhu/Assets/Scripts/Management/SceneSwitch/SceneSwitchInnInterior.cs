using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchInnInterior : MonoBehaviour
{
    public GameObject button;

    void OnTriggerStay2D(Collider2D other)
    {       
        if (other.CompareTag("Player"))
        {
            button.SetActive(true);

            if (Input.GetButtonDown("Interract"))
            {
                LevelManager.Instance.InnInterior();
            }            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(false);           
        }
    }
}
