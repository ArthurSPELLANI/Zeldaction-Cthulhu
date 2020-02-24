using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {

        public Transform[] attackPos;
        Transform currentAttackPos;
        Vector2 currentDirection;
        [Range(0, 1)]
        public float attackRange;
        public LayerMask enemyLayer;
        public int attackCount = 0;

        [HideInInspector] public bool canAttack;
        

    
    	void Awake()
	    {
	        
	    }
    
        void Start()
        {
            
        }
    
        void Update()
        {
            currentDirection = PlayerManager.Instance.playerMovement.currentDirection;            

            if (Input.GetButtonDown("Attack"))
            {
                StartCoroutine(PlayerManager.Instance.playerMovement.AttackDash());
            }
        }
    
        //Trouver la position d'attaque pour le premier coup
        void GetAttackPos1()
        {
            if (currentDirection.x == 1)
            {
                currentAttackPos = attackPos[1];
            }

            if (currentDirection.x == -1)
            {
                currentAttackPos = attackPos[5];
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[7];
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[3];
            }
        }

        //Trouver la position d'attaque pour le second coup
        void GetAttackPos2()
        {
            if (currentDirection.x == 1)
            {
                currentAttackPos = attackPos[3];
            }

            if (currentDirection.x == -1)
            {
                currentAttackPos = attackPos[7];
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[1];
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[5];
            }
        }

        //Trouver la position d'attaque pour le troisième coup
        void GetAttackPos3()
        {
            if (currentDirection.x == 1)
            {
                currentAttackPos = attackPos[2];
            }

            if (currentDirection.x == -1)
            {
                currentAttackPos = attackPos[6];
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[0];
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[4];
            }
        }

        //Choisi le type d'attaque en fonction du placement dans la séquence de coups
        public void AttackManager()
        {
            if (attackCount == 0)
            {
                Attack1();
            }

            else if (attackCount == 1)
            {
                Attack2();
            }

            else if (attackCount == 2)
            {
                Attack3();
            }
        }

        //Premier coup de la série d'attaques
        void Attack1()
        {
            GetAttackPos1();            
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);            
            attackCount += 1;
        }

        //Second coup de la série d'attaques
        void Attack2()
        {
            GetAttackPos2();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);            
            attackCount += 1;
        }

        //Troisième (et dernier) coup de la série d'attaques
        void Attack3()
        {
            GetAttackPos3();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange / 2, enemyLayer);            
            attackCount = 0;
        }

        private void OnDrawGizmosSelected()
        {
            if (currentAttackPos == null)
                return;

            Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
        }

    }
}