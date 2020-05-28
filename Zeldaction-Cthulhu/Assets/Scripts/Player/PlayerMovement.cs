using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManaging;

namespace Player
{

    public class PlayerMovement : MonoBehaviour
    {
        [Range(0, 100)]
        public int speed;

        [SerializeField]
        public bool isWalking = false;
        

        public Rigidbody2D playerRb;

        float vertical;
        float horizontal;
        Vector2 direction;

        public Vector2 currentDirection;

        public Animator animator;

        public bool canMove = true;
        private bool isDashing = false;

        [Range(0, 1)]
        public float dashRecoil;

        public AnimationCurve dashCurve;

        public GameObject pufpuf;

        void Awake()
        {
                       
        }

        void Start()
        {
            playerRb = GetComponentInParent<Rigidbody2D>();
            animator = PlayerManager.Instance.playerAnimator;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);         
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


            SoundRunning();

            //Reset de la vitesse de déplacement si le joueur ne bouge plus
            if (canMove == false && isDashing == false)
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
            if (horizontal != 0 || vertical != 0)
            {
                animator.SetBool("IsWalking", true);
                isWalking = true;
            }
            else if (horizontal == 0 && vertical == 0)
            {
                animator.SetBool("IsWalking", false);
                isWalking = false;
            }            
        }

        //Fonction qui store la dernière direction du joueur + Direction de l'animator
        private void GetDirection()
        {

            if(horizontal != 0 || vertical != 0)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    currentDirection = new Vector2(Mathf.RoundToInt(direction.x), 0);
                }
                else
                {
                    currentDirection = new Vector2(0, Mathf.RoundToInt(direction.y));
                }
            }                                    

            if (PlayerManager.Instance.playerShoot.isAiming == false)
            {
                //Set de la direction de l'animator
                animator.SetFloat("Horizontal", currentDirection.x);
                animator.SetFloat("Vertical", currentDirection.y);
            }
            else
            {
                animator.SetFloat("AimHorizontal", currentDirection.x);
                animator.SetFloat("AimVertical", currentDirection.y);
            }
        }

        //Coroutine lancée après l'input d'attaque, lance la fonction d'attaque dans le script PlayerAttack
        public IEnumerator AttackDash(float dashSpeed, float dashTime)
        {

            //must be the same value as in AttackRedirection function + 0.01f !!!
            yield return new WaitForSeconds(0.11f);

            isDashing = true;
            float timer = 0.0f;

            Vector2 aim = currentDirection;

            while (timer < dashTime)
            {
                playerRb.velocity = aim.normalized * (dashSpeed * dashCurve.Evaluate(timer / dashTime));

                timer += Time.deltaTime;
                yield return null;
            }

            playerRb.velocity = Vector2.zero;
            isDashing = false;
        }

        #region Sound
        float timeBetweenStep = 0.5f;
        private float currentTime = 0;
        void SoundRunning()
        {
            if (playerRb.velocity != Vector2.zero)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= timeBetweenStep)
                {
                    AudioManager.Instance.CoursePierre();
                    currentTime = 0;
                }
            }
        }
        #endregion 
    }
}