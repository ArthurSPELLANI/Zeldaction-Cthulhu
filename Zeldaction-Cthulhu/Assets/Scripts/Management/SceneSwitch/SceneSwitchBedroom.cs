using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Game;

public class SceneSwitchBedroom : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetButtonDown("Interract"))
        {
            LevelManager.Instance.Bedroom();
        }
    }
}
