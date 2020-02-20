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

    
    	void Awake()
	    {
	        
	    }
    
        void Start()
        {
            
        }
    
        void Update()
        {
            currentDirection = PlayerManager.Instance.playerMovement.currentDirection;            

            if (Input.GetKeyDown(KeyCode.Space) && attackCount == 0)
            {
                Attack1();
            }

            else if (Input.GetKeyDown(KeyCode.Space) && attackCount == 1)
            {
                Attack2();
            }

             else if (Input.GetKeyDown(KeyCode.Space) && attackCount == 2)
            {
                Attack3();
            }
        }
    
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


        void Attack1()
        {
            GetAttackPos1();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);
            attackCount += 1;
        }

        void Attack2()
        {
            GetAttackPos2();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);
            attackCount += 1;
        }

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