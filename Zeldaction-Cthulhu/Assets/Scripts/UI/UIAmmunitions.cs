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


    private void Start()
    {

    }

    void Update()
    {
        ammo = PlayerManager.Instance.playerShoot.ammunitions -1;

        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < ammo)
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
