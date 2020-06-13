using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.SceneManagement;

public class SceneSwitchCorruptedForest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
    }
}
