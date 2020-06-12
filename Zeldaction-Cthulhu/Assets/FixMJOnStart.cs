using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FixMJOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        PlayerManager.Instance.playerAnimator.SetFloat("Horizontal", PlayerManager.Instance.playerMovement.currentDirection.x);
        PlayerManager.Instance.playerAnimator.SetFloat("Vertical", PlayerManager.Instance.playerMovement.currentDirection.y);
    }   
}
