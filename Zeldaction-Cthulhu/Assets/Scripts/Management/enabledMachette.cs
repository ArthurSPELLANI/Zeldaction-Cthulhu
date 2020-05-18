using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class enabledMachette : MonoBehaviour
{
    public void machetteOn ()
    {
        PlayerManager.Instance.playerAttack.enabled = true;
    }
}
