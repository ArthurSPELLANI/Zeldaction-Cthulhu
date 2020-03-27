﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {

        [HideInInspector] public Vector2 shootDirection;
        float aimHorizontal;
        float aimVertical;

        bool directionStored = false;
        bool canShoot;        

        [Range(0,50)]
        public int aimSlow;

        [Range(0, 20)]
        public int bulletSpeed;

        public GameObject bullet;
        public float shootCooldown;
        public int ammunitions = 3;

        Animator animator;

        Quaternion rotation = Quaternion.identity;

        [HideInInspector] public bool isAiming = false;


        void Awake()
	    {
	    
	    }
    
        void Start()
        {
            canShoot = true;
            animator = PlayerManager.Instance.GetComponentInChildren<Animator>();
        }
    
        void Update()
        {
            aimHorizontal = shootDirection.normalized.x;
            aimVertical = shootDirection.normalized.y;


            if (Input.GetAxisRaw("Shoot") != 0)
            {
               AimMovement();
               animator.SetBool("isAiming", true);
            }

            if (Input.GetAxisRaw("Shoot") == 0 && isAiming == true)
            {              
               ExitShoot();
            }
        }

        void AimMovement()
        {            
            if(directionStored == false)
            {
                GetDirection();
            }



            if (Input.GetButtonDown("Attack") && canShoot == true)
            {
                if(ammunitions > 0)
                {
                    animator.SetBool("isShooting", true);
                    ShootBullet();
                }                
            }

        }
    
        void GetDirection()
        {
            shootDirection = PlayerManager.Instance.playerMovement.currentDirection;
            PlayerManager.Instance.playerMovement.speed -= aimSlow;
            directionStored = true;
            isAiming = true;
        }

        void ExitShoot()
        {
            directionStored = false;
            PlayerManager.Instance.playerMovement.speed += aimSlow;
            animator.SetBool("isAiming", false);
            isAiming = false;
        }

        void ShootBullet()
        {
            StartCoroutine(ShootDelay());
            ammunitions -= 1;
            Instantiate(bullet, this.transform.position, rotation);
        }

        IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("isShooting", false);
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;            
        }

    }
}