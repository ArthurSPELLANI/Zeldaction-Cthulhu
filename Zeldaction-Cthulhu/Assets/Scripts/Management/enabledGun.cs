using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class enabledGun : MonoBehaviour
{
    public void gunOn ()
    {
        PlayerManager.Instance.playerShoot.enabled = true;
    }
}
