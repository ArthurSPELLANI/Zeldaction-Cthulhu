using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class UIHearts : MonoBehaviour
{
    int health;    

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    private void Start()
    {
       // health = PlayerManager.Instance.playerStats.playerCurrentHealth;
    }

    void Update()
    {
        health = PlayerManager.Instance.playerStats.playerCurrentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i == health)
            {
                hearts[i].sprite = halfHeart;
            }
            else if(i > health)
            {
                hearts[i].sprite = emptyHeart;
            }             
        }
    }
}
