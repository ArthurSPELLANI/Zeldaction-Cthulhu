using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PickupFragment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerShadowMode.fragment += 1;
            Destroy(gameObject);
        }
    }
}
