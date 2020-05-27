using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Enemy;
using Boss;
using PillarSystem;
using AudioManaging;
//using LevelDesignElement;

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
        public float coolDown = 0;
        public float attackKnockback;

        [HideInInspector] public Animator animator;
        [HideInInspector] public PlayerMovement movementScript;
        private Rigidbody2D playerRb;

        [HideInInspector] public bool cantAttack = false;

        private CapsuleDirection2D attack3Dir;
        private Vector2 attack3Size;
        public float attack3VertSize;
        public float attack3HoriSize;

        [Range(0, 10)]
        public float dashSpeed;
        [Range(0, 2)]
        public float dashTime;

        


        void Awake()
	    {
            this.enabled = false;
	    }

        private void OnEnable()
        {
            animator = PlayerManager.Instance.playerAnimator;
            movementScript = PlayerManager.Instance.playerMovement;
            playerRb = transform.parent.parent.GetComponent<Rigidbody2D>();
        }
    
        void Update()
        {
            currentDirection = PlayerManager.Instance.playerMovement.currentDirection;
            
            if (cantAttack == false)
            {
                coolDown -= Time.deltaTime;
            }

            

            if (PlayerManager.Instance.playerShoot.isAiming == false && PlayerManager.Instance.playerShadowMode.isShadowActivated == false)
            {
                //Si le joueur appuie sur le bouton d'attaque, lance une coroutine dans le script PlayerMovement
                if (Input.GetButtonDown("Attack") && cantAttack == false)
                {
                    AnimatorManager();
                    cantAttack = true;
                    movementScript.canMove = false;
                }
                
            }
            
            /*if(cantAttack == false)
            {
                animator.SetBool("IsAttacking", false);
            }*/


            if(coolDown <= 0 && attackCount != 0)
            {
                attackCount = 0;
                playerDamage = 1;
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
                currentAttackPos = attackPos[3];
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[0];
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[2];
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
                currentAttackPos = attackPos[3];
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[0];
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[2];
            }
        }

        //Trouver la position d'attaque pour le troisième coup
        void GetAttackPos3()
        {
            if (currentDirection.x == 1)
            {
                currentAttackPos = attackPos[5];
                attack3Dir = CapsuleDirection2D.Horizontal;
            }

            if (currentDirection.x == -1)
            {
                currentAttackPos = attackPos[7];
                attack3Dir = CapsuleDirection2D.Horizontal;
            }

            if (currentDirection.y == 1)
            {
                currentAttackPos = attackPos[4];
                attack3Dir = CapsuleDirection2D.Vertical;
            }

            if (currentDirection.y == -1)
            {
                currentAttackPos = attackPos[6];
                attack3Dir = CapsuleDirection2D.Vertical;
            }
        }

        //Choisi le type d'attaque en fonction du placement dans la séquence de coups
        public IEnumerator AttackManager()
        {
            movementScript.canMove = true;
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

            yield return new WaitForSeconds(0.015f);

            movementScript.canMove = false;
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (attackCount == 0)
            {
                Attack1();
                coolDown = comboKeepTime;
            }

            else if (attackCount == 1)
            {
                Attack2();
                coolDown = comboKeepTime;
            }

            else if (attackCount == 2)
            {
                Attack3();
                coolDown = comboKeepTime;
            }
        }

        //Premier coup de la série d'attaques
        void Attack1()
        {
            Debug.Log("l'attaque 1 est lancée");

            GetAttackPos1();

            AudioManager.Instance.Play("coup1");

            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage, transform.position, attackKnockback);
                    AudioManager.Instance.Play("impactEnnemi");
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }

                if (enemy.CompareTag("DoorCave"))
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            Collider2D pillar = Physics2D.OverlapCircle(currentAttackPos.position, attackRange, pillarLayer);

            if (pillar != null)
            {
                pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
                AudioManager.Instance.Play("tappagePillier");
            }

            Collider2D[] hitRonces = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, roncesLayer);

            foreach(Collider2D ronces in hitRonces)
            {
                ronces.GetComponent<Ronces>().Destroy();
                AudioManager.Instance.Play("cassageBuisson");
            }
        }

        //Second coup de la série d'attaques
        void Attack2()
        {
            Debug.Log("l'attaque 2 est lancée");

            GetAttackPos2();

            //StartCoroutine(PlayerManager.Instance.playerMovement.AttackDash(dashSpeed, dashTime));

            AudioManager.Instance.Play("coup2");
            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, enemyLayer);



            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage, transform.position, attackKnockback);
                    AudioManager.Instance.Play("impactEnnemi");
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }

                if (enemy.CompareTag("DoorCave"))
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            Collider2D pillar = Physics2D.OverlapCircle(currentAttackPos.position, attackRange, pillarLayer);

            if (pillar != null)
            {
                pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
                AudioManager.Instance.Play("tappagePillier");

            }

            Collider2D[] hitRonces = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRange, roncesLayer);

            foreach (Collider2D ronces in hitRonces)
            {
                ronces.GetComponent<Ronces>().Destroy();
                AudioManager.Instance.Play("cassageBuisson");

            }

        }

        //Troisième (et dernier) coup de la série d'attaques
        void Attack3()
        {
            Debug.Log("l'attaque 3 est lancée");

            GetAttackPos3();

            //StartCoroutine(PlayerManager.Instance.playerMovement.AttackDash(dashSpeed * 2, dashTime));

            AudioManager.Instance.Play("coup3");

            
            if(attack3Dir == CapsuleDirection2D.Horizontal)
            {
                attack3Size = new Vector2(attack3VertSize, attack3HoriSize);
            }
            else
            {
                attack3Size = new Vector2(attack3HoriSize, attack3VertSize);
            }

            //Detect enemies in a range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCapsuleAll(currentAttackPos.position, attack3Size, attack3Dir, enemyLayer);


            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyBasicBehavior>().TakeDamage(playerDamage, transform.position, attackKnockback * 1.5f);
                    AudioManager.Instance.Play("impactEnnemi");
                }

                if (enemy.CompareTag("Boss"))
                {
                    enemy.transform.parent.GetComponentInParent<BossBaseBehavior>().BossTakeDamage();
                }

                if (enemy.CompareTag("DoorCave"))
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            Collider2D pillar = Physics2D.OverlapCapsule(currentAttackPos.position, attack3Size, attack3Dir, pillarLayer);

            if (pillar != null)
            {
                
                if (pillar.CompareTag("pillar"))
                {
                    pillar.GetComponent<Pillar>().CorruptionBeam(currentDirection);
                    AudioManager.Instance.Play("tappagePillier");
                }
                
            }


            Collider2D[] hitRonces = Physics2D.OverlapCapsuleAll(currentAttackPos.position, attack3Size, attack3Dir, roncesLayer);

            foreach (Collider2D ronces in hitRonces)
            {
                if (ronces.CompareTag("Enviro"))
                {
                    ronces.GetComponent<Ronces>().Destroy();
                    AudioManager.Instance.Play("cassageBuisson");
                }
                
            }

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

        public void AttackEndEvent()
        {
            if (attackCount != 2)
            {
                if (attackCount == 1)
                {
                    playerDamage++;
                }

                attackCount++;
            }
            else
            {
                attackCount = 0;
                playerDamage--;
            }

            animator.SetBool("IsAttacking", false);

            //laisser ces trucs à la fin de la fonction
            cantAttack = false;
            movementScript.canMove = true;
        }





        #region DrawGizmos Function
        private void OnDrawGizmosSelected()
        {
            if (currentAttackPos == null)
                return;

            if (attackCount == 0)
            {
                Gizmos.color = Color.red;
                if (attack3Dir == CapsuleDirection2D.Horizontal)
                {
                    //Hozirontal
                    Gizmos.DrawLine(new Vector2(currentAttackPos.position.x - attack3VertSize / 2, currentAttackPos.position.y), new Vector2(currentAttackPos.position.x + attack3VertSize / 2, currentAttackPos.position.y));
                    //Vertical
                    Gizmos.DrawLine(new Vector2(currentAttackPos.position.x, currentAttackPos.position.y - attack3HoriSize / 2), new Vector2(currentAttackPos.position.x, currentAttackPos.position.y + attack3HoriSize / 2));
                }
                else
                {
                    //Hozirontal
                    Gizmos.DrawLine(new Vector2(currentAttackPos.position.x - attack3HoriSize / 2, currentAttackPos.position.y), new Vector2(currentAttackPos.position.x + attack3HoriSize / 2, currentAttackPos.position.y));
                    //Vertical
                    Gizmos.DrawLine(new Vector2(currentAttackPos.position.x, currentAttackPos.position.y - attack3VertSize / 2), new Vector2(currentAttackPos.position.x, currentAttackPos.position.y + attack3VertSize / 2));
                }
                
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(currentAttackPos.position, attackRange);
                Gizmos.DrawLine(currentAttackPos.position, transform.position);
            }
          
        }
        #endregion

    }
}