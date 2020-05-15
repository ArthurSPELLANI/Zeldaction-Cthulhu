using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menu;

public class GetNearEnemyDetection : MonoBehaviour
{
    void Start()
    {
        PauseMenu.Instance.Save = this.gameObject;
    }
}
