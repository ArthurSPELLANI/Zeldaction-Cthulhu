using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class SceneSwitchTownNight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.TownNight();
    }
}
