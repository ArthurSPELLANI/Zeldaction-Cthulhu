using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchLightForest2 : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.LightForest2();
    }
    
    IEnumerator ShetanLightForest2()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.LightForest2();
    }
}
