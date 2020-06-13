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
                StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, 32));
            }

            if(east == true)
            {
                StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, 33));
            }

            if(west == true)
            {
                StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, 31));
            }
        }
    }
}
