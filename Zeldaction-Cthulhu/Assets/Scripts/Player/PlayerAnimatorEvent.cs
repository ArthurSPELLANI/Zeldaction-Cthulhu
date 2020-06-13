using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Game;

public class PlayerAnimatorEvent : MonoBehaviour
{
    public PlayerAttack attack;
    public PlayerMovement movement;
    public PlayerShoot shoot;

    private void Start()
    {
        movement = PlayerManager.Instance.playerMovement;
        shoot = PlayerManager.Instance.playerShoot;
    }

    public void AnimatorAttack()
    {
        attack.AttackManager();
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

    public void AnimatorCanShoot()
    {
        shoot.canShoot = true;
    }

    public void AnimatorCantShoot()
    {
        shoot.canShoot = false;
    }

    public void AnimatorAttackPrep()

    {



        StartCoroutine(attack.AttackRedirection());



        if (attack.attackCount == 0)

        {

            StartCoroutine(movement.AttackDash(attack.dashSpeed / 2, attack.dashTime));

        }

        else if (attack.attackCount == 1)

        {

            StartCoroutine(movement.AttackDash(attack.dashSpeed, attack.dashTime));

        }

        else if(attack.attackCount == 2)

        {

            StartCoroutine(movement.AttackDash(attack.dashSpeed * 2, attack.dashTime));

        }



        

    }

    public void GameOver()
    {
        UIManager.Instance.gameOver.SetActive(true);
    }

}
