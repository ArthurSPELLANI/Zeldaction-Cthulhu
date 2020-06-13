using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.SceneManagement;

public class SceneSwitchTownDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
        }
    }
}

