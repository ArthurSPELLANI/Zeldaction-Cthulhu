using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerAnimatorEvent : MonoBehaviour
{
    public PlayerAttack attack;
    public PlayerMovement movement;
    public PlayerShoot shoot;

    public void AnimatorAttack()
    {
        StartCoroutine(attack.AttackManager());
    }

    public void AnimatorCantMove()
    {
        movement.canMove = false;
    }

    public void AnimatorCanMove()
    {
        movement.canMove = true;
    }

    public void AnimatorEndAttack()
    {
        attack.AttackEndEvent();
    }

}
