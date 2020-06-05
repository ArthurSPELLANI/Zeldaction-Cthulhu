using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Boss;

public class Vortex : MonoBehaviour
{
    public Animator animator;
    GameObject player;
    public GameObject pullPoint;
    public float pullForce;
    public LayerMask playerLayer;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tower4;

    bool canPull = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = PlayerManager.Instance.gameObject;
        animator.SetBool("isActive", true);
        canPull = true;
    }

    private void OnEnable()
    {
        animator.SetBool("isActive", true);
        canPull = true;
    }

    private void Update()
    {
        if (player.transform.position != pullPoint.transform.position && canPull == true)
        {
            player.GetComponent<Rigidbody2D>().AddForce((pullPoint.transform.position - player.transform.position) * pullForce);

            //safe zone around the pullPoint
            Collider2D hitPlayer = Physics2D.OverlapCircle(pullPoint.transform.position, 0.1f, playerLayer);

            if (hitPlayer != null)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }

        if (tower1.activeSelf == true || tower2.activeSelf == true || tower3.activeSelf == true || tower4.activeSelf == true)
        {
            animator.SetBool("isActive", false);
            canPull = false;
        }



    }

}
