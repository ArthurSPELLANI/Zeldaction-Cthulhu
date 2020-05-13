using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitch_12 : MonoBehaviour
{
    public bool playerInRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange || Input.GetButtonDown("Interract") && playerInRange)
        {
            LevelManager.Instance.Playtest_12();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
