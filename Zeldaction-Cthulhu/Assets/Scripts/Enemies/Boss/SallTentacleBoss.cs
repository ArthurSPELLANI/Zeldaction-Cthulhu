﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class SallTentacleBoss : MonoBehaviour
{
    Animator animator;
    CapsuleCollider2D capsCollider;
    public Phase1PatternManager p1Man;
    public BossBaseBehavior boss;
    bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.isInPhase1 == true)
        {
            if (p1Man.patternNbr == 2 && isActive == false)
            {
                isActive = true;
                StartCoroutine(StartTentacle());
            }
            else if (p1Man.patternNbr != 2)
            {
                animator.SetBool("isActive", false);
                capsCollider.enabled = false;
                isActive = false;
            }
        }
        else
        {
            animator.SetBool("isActive", false);
            capsCollider.enabled = false;
            isActive = false;
        }


        IEnumerator StartTentacle()
        {            
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            animator.SetBool("isActive", true);
            capsCollider.enabled = true;
        }

        
    }
}
