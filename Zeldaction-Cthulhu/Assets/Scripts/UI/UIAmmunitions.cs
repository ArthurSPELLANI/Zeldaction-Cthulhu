using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class UIAmmunitions : MonoBehaviour
{
    int ammo;

    public Image[] bullets;
    public Sprite fullBullet;
    public Sprite emptyBullet;
    public Image backBullet;

    bool isShooting = false;


    private void Start()
    {
        
    }

    void Update()
    {
        ammo = PlayerManager.Instance.playerShoot.ammunitions -1;
        isShooting = PlayerManager.Instance.playerShoot.isAiming;

        if (isShooting == true)
        {
            backBullet.gameObject.SetActive(true);
        }
        else
            backBullet.gameObject.SetActive(false);

        for (int i = 0; i < bullets.Length; i++)
        {
            if (i <= ammo)
            {
                bullets[i].sprite = fullBullet;
            }
            
            else if (i > ammo)
            {
                bullets[i].sprite = emptyBullet;
            }



        }
    }
}
