using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerAnimatorEvent : MonoBehaviour
{
    public PlayerAttack attack;

    public void AnimatorAttack()
    {
        attack.AttackManager();
    }

}
