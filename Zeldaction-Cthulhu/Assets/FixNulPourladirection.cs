using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FixNulPourladirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(0, 1);
    }

    
}
