using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Game;

public class SceneSwitchBedroom : MonoBehaviour
{
    public GameObject button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetButtonDown("Interract"))
        {
            LevelManager.Instance.Bedroom();
        }
    }
}
