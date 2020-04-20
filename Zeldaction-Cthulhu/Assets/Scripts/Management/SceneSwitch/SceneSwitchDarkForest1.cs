using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchDarkForest1 : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.DarkForest1();
        }
    }
}
