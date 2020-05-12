using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchTownDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LevelManager.Instance.TownDestroyed();
        }
    }
}

