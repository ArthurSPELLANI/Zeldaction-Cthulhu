using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchCaveToDF : MonoBehaviour
{

    public bool south;
    public bool east;
    public bool west;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(south == true)
            {
                LevelManager.Instance.SouthDF();
            }

            if(east == true)
            {
                LevelManager.Instance.EastDF();
            }

            if(west == true)
            {
                LevelManager.Instance.WestDF();
            }
        }
    }
}
