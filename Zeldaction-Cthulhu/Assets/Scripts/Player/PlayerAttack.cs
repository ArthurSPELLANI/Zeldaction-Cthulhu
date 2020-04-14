using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Boss;

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
        public LayerMask pillarLayer;
        public LayerMask roncesLayer;
        public int attackCount = 0;
        public int playerDamage = 1;
        public float comboKeepTime;
        float coolDown = 0;

        Pillar pillar;
        Ronces ronces;

        Animator animator;

        [HideInInspector] public bool cantAttack = false;
        

    
    	void Awake()
	    {
	        
	    }
    
        void Start()
        {
            animator = PlayerManager.Instance.GetComponentInChildren<Animator>();
        }
    
        void Update()
        {
            currentDirection = PlayerManager.Instance.playerMovement.currentDirection;

            coolDown -= Time.deltaTime;

            if (PlayerManager.Instance.playerShoot.isAiming == false && PlayerManager.Instance.playerShadowMode.isShadowActivated == false)
            {
                //Si le joueur appuie sur le bouton d'attaque, lance une coroutine dans le script PlayerMovement
                if (Input.GetButtonDown("Attack") && cantAttack == false)
                {
                    if(attackCount != 2)
                    {
                        StartCoroutine(PlayerManager.Instance.playerMovement.AttackDashShort());
                    }

                    else if (attackCount == 2)
                    {
                        StartCoroutine(PlayerManager.Instance.playerMovement.AttackDashLong());
                    }        
                }
                
            }
            
            if(cantAttack == false)
            {
                animator.SetBool("IsAttacking", false);
            }

            if(coolDown == 0 && attackCount != 0)
            {
                attackCount = 0;
            }

        }
    
        //Trouver la position d'attaque pour le premier coup
        void GetAttackPos1()
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

        //Trouver la position d'attaque pour le second coup
        void GetAttackPos2()
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

            if (cantAttack == false)
            {
                animator.SetBool("IsAttacking", false);
            }

            if (coolDown > 0)
                return;

            coolDown = comboKeepTime;

        }

        //Premier coup de la série d'attaques
        void Attack1()
        {
            GetAttackPos1();            
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage);
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }
            }

            Collider2D pillar = Physics2D.OverlapCircle(currentAttackPos.position, attackRange, pillarLayer);

            if (pillar != null)
            {
                pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
            }

            Collider2D[] hitRonces = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, roncesLayer);

            foreach(Collider2D ronces in hitRonces)
            {
                ronces.GetComponent<Ronces>().Destroy();
            }

            attackCount += 1;
        }

        //Second coup de la série d'attaques
        void Attack2()
        {
            GetAttackPos2();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);



            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage);
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }
            }

            Collider2D pillar = Physics2D.OverlapCircle(currentAttackPos.position, attackRange, pillarLayer);

            if (pillar != null)
            {
                pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
            }

            Collider2D[] hitRonces = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, roncesLayer);

            foreach (Collider2D ronces in hitRonces)
            {
                ronces.GetComponent<Ronces>().Destroy();
            }


            attackCount += 1;
            playerDamage++;
        }

        //Troisième (et dernier) coup de la série d'attaques
        void Attack3()
        {
            GetAttackPos3();
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange / 2, enemyLayer);



            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage);
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }
            }

            Collider2D pillar = Physics2D.OverlapCircle(currentAttackPos.position, attackRange, pillarLayer);

            if (pillar != null)
            {
                pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
            }


            Collider2D[] hitRonces = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, roncesLayer);

            foreach (Collider2D ronces in hitRonces)
            {
                ronces.GetComponent<Ronces>().Destroy();
            }

            attackCount = 0;
            playerDamage--;
        }

        public void AnimatorManager()
        {
            if(attackCount == 0)
            {
                animator.SetInteger("AttackCount", 1);
                animator.SetBool("IsAttacking", true);
            }

            else if(attackCount == 1)
            {
                animator.SetInteger("AttackCount", 2);
                animator.SetBool("IsAttacking", true);
            }

            else if(attackCount == 2)
            {
                animator.SetInteger("AttackCount", 3);
                animator.SetBool("IsAttacking", true);
            }

        }

        private void OnDrawGizmosSelected()
        {
            if (currentAttackPos == null)
                return;

            Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
        }
    }
}