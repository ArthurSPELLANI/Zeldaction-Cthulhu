using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using AudioManaging;

public class PickupFragment : MonoBehaviour
{
    [HideInInspector]
    public int exist = 1;
    public GameObject particle;

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
            AudioManager.Instance.Play("rangementPistolet");
            Instantiate(particle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            exist = 0;
        }
    }
}
