using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.SceneManagement;

public class SceneSwitchLightForest2 : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator ShetanLightForest2()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UIManager.Instance.fadeSwitch.FadeToIn(1, 0.7f, SceneManager.GetActiveScene().buildIndex));
    }
}
