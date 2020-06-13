using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.SceneManagement;

public class SceneSceneUpstairInn : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
        }
    }
}
