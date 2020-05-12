using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class StartForLightForest2 : MonoBehaviour
{

    void Start()
    {
        PlayerManager.Instance.baseUI.SetActive(true);
        PlayerManager.Instance.playerShoot.ammunitions = 0;
    }


}
