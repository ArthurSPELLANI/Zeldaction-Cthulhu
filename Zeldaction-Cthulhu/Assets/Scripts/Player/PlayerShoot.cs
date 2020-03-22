using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {

        [HideInInspector] public Vector2 shootDirection;
        bool directionStored = false;
        bool canShoot;

        [Range(0,50)]
        public int aimSlow;

        [Range(0, 20)]
        public int bulletSpeed;

        public GameObject bullet;
        public float shootCooldown;
        public int ammunitions = 3;

        [HideInInspector] public bool isShooting = false;


        void Awake()
	    {
	    
	    }
    
        void Start()
        {
            canShoot = true;
        }
    
        void Update()
        {

            if (Input.GetAxisRaw("Shoot") != 0)
            {
               AimMovement();                
            }

            if (Input.GetAxisRaw("Shoot") == 0 && isShooting == true)
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
                    ShootBullet();
                }                
            }

        }
    
        void GetDirection()
        {
            shootDirection = PlayerManager.Instance.playerMovement.currentDirection;
            PlayerManager.Instance.playerMovement.speed -= aimSlow;
            directionStored = true;
            isShooting = true;
        }

        void ExitShoot()
        {
            directionStored = false;
            PlayerManager.Instance.playerMovement.speed += aimSlow;
            isShooting = false;
        }

        void ShootBullet()
        {            
            StartCoroutine(ShootDelay());
            ammunitions -= 1;
            Instantiate(bullet, this.transform);
        }

        IEnumerator ShootDelay()
        {
            canShoot = false;
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
        }

    }
}