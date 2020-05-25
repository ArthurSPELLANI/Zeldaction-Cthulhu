using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ReceiverForCurrentDirection : MonoBehaviour
{


    public void SetCurrentDirectionRight()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(1, 0);
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void SetCurrentDirectionLeft()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(-1, 0);
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void SetCurrentDirectionUp()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(0, 1);
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void SetCurrentDirectionDown()
    {
        PlayerManager.Instance.playerMovement.currentDirection = new Vector2(0, -1);
        PlayerManager.Instance.playerAnimator.gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void ShowUI()
    {
        PlayerManager.Instance.baseUI.SetActive(true);
    }
}
