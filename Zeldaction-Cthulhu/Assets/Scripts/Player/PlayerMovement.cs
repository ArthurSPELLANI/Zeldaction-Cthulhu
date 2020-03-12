using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(0, 100)]
        public int speed;

        

        public Rigidbody2D playerRb;

        float vertical;
        float horizontal;
        Vector2 direction;

        [HideInInspector] public Vector2 currentDirection;

        Animator animator;

        public bool canMove = true;

        [Range(0, 2)]
        public float dashTime;
        [Range(0, 2)]
        public float dashDelay;
        [Range(0, 10)]
        public float dashSpeed;

        public AnimationCurve dashCurve;

        void Awake()
        {
            playerRb = GetComponentInParent<Rigidbody2D>();            
        }

        void Start()
        {
            animator = PlayerManager.Instance.GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
         
            //Mouvement du joueur + store de la direction
            if (canMove == true && PlayerManager.Instance.playerShadowMode.isShadowActivated == false)
            {
                PlayerMove();
                GetDirection();
            }
        }

        void Update()
        {

            //Store des valeur d'input du joystick gauche
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");


                        

            //Reset de la vitesse de déplacement si le joueur ne bouge plus
            if(canMove == false)
            {
                playerRb.velocity = direction * 0 * Time.fixedDeltaTime;
            }

        }

        //Fonction qui gère le déplacement en 8 directions du personnage
        private void PlayerMove()
        {
            if (vertical >= 0.01)
            {
                vertical = 1;
            }
            else if (vertical <= -0.01)
            {
                vertical = -1;
            }
            else
            {
                vertical = 0;
            }

            if (horizontal >= 0.01)
            {
                horizontal = 1;
            }
            else if (horizontal <= -0.01)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 0;
            }

            //Set de la vecocity en fonction de la direction du joueur
            direction = new Vector2(horizontal, vertical).normalized;
            playerRb.velocity = direction * speed * Time.fixedDeltaTime;


            //Set de l'animator si le joueur est en mouvement
            if(horizontal != 0 || vertical != 0)
            {
                animator.SetBool("IsWalking", true);
            }
            else if (horizontal == 0 && vertical == 0)
            {
                animator.SetBool("IsWalking", false);
            }

        }

        //Fonction qui store la dernière direction du joueur + Direction de l'animator
        private void GetDirection()
        {
            if(direction.x == 1 && direction.y == 0)
            {
                currentDirection = new Vector2(1, 0);
            }
            if (direction.x == -1 && direction.y == 0)
            {
                currentDirection = new Vector2(-1, 0);
            }
            if (direction.x == 0 && direction.y == 1)
            {
                currentDirection = new Vector2(0, 1);
            }
            if (direction.x == 0 && direction.y == -1)
            {
                currentDirection = new Vector2(0, -1);
            }

            //Set de la direction de l'animator
            animator.SetFloat("Horizontal", currentDirection.x);
            animator.SetFloat("Vertical", currentDirection.y);

        }

        //Coroutine lancée après l'input d'attaque, lance la fonction d'attaque dans le script PlayerAttack
        public IEnumerator AttackDash()
        {
            float timer = 0.0f;

            Vector2 aim = currentDirection;
            canMove = false;
            PlayerManager.Instance.playerAttack.canAttack = true;

            yield return new WaitForSeconds(dashDelay);

            PlayerManager.Instance.playerAttack.AttackManager();

            while (timer < dashTime)
            {
                playerRb.velocity = aim.normalized * (dashSpeed * dashCurve.Evaluate(timer / dashTime));

                timer += Time.deltaTime;
                yield return null;
            }

            playerRb.velocity = Vector2.zero;
            canMove = true;
            PlayerManager.Instance.playerAttack.canAttack = false;
        }

    }
}