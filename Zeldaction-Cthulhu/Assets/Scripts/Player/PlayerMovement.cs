using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(0, 100)]
        public int speed;

        

        Rigidbody2D playerRb;

        float vertical;
        float horizontal;
        Vector2 direction;

        [HideInInspector] public Vector2 currentDirection;


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

        }

        void Update()
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            if(canMove == true)
            {         
                PlayerMove();
                GetDirection();
            }            

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

            direction = new Vector2(horizontal, vertical).normalized;
            playerRb.velocity = direction * speed * Time.fixedDeltaTime;
        }

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

        }


        public IEnumerator AttackDash()
        {
            float timer = 0.0f;

            Vector2 aim = currentDirection;
            canMove = false;            

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
        }

    }
}