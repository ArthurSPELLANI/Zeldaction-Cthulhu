using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class PlayerShadowMovement : MonoBehaviour
    {
        [Range(0, 100)]
        public int shadowSpeed;

        float vertical;
        float horizontal;
        Vector2 direction;
        Vector2 currentDirection;

        public Animator animator;

        public Rigidbody2D shadowRb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            ShadowMove();
            GetDirection();
            
            if (shadowRb.velocity.x != 0 || shadowRb.velocity.y != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }

        private void ShadowMove()
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
            shadowRb.velocity = direction * shadowSpeed;
        }

        private void GetDirection()
        {
            if (direction.x == 1 && direction.y == 0)
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
    }
}


