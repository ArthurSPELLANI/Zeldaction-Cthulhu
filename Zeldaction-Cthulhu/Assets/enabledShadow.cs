using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class enabledShadow : MonoBehaviour
{
   public void shadowOn()
    {
        PlayerManager.Instance.playerAttack.enabled = true;
    }
}
