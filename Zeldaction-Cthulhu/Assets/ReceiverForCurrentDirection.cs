using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ReceiverForCurrentDirection : MonoBehaviour
{


    public void SetCurrentDirection()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(1, 0);
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }
}
