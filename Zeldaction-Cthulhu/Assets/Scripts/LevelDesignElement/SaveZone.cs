using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

//Cette class sert à sauvegarder la progression du joueur grâce à des collider
public class SaveZone : MonoBehaviour
{
    WorldSave worldSave;
    public PlayerSave playerSave;

    void Start()
    {
        worldSave = GetComponentInParent<WorldSave>();
        playerSave = GetComponentInParent<PlayerSave>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            worldSave.SaveFragment();
            worldSave.SavePillar();
            worldSave.SaveGameObjectActivation();
            playerSave.Save();
        }
    }
}
