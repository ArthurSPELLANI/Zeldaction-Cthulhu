using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PickupFragment : MonoBehaviour
{
    [HideInInspector]
    public int exist = 1;

    void Start()
    {
        if (exist == 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerShadowMode.fragment += 1;
            gameObject.SetActive(false);
            exist = 0;
        }
    }
}
