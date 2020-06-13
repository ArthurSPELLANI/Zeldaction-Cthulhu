using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

using UnityEngine.SceneManagement;

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
                StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
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
