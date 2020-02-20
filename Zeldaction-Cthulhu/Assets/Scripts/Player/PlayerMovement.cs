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

            PlayerMove();
            GetDirection();
      
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


    }
}