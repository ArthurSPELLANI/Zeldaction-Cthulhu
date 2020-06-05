using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;
using Player;

public class MamyTransition : MonoBehaviour
{
    public BossBaseBehavior boss;

    public void Phase2Begin()
    {
        boss.Phase2Begin();
    }

    private void Update()
    {
        PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.localPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2f);
    }
}
