using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Game;

//Cette class sert à sauvegarder la progression du joueur grâce à des collider
public class SaveZone : MonoBehaviour
{
    WorldSave worldSave;
    public PlayerSave playerSave;
    GameObject savingText;

    void Start()
    {
        worldSave = GetComponentInParent<WorldSave>();
        playerSave = GetComponentInParent<PlayerSave>();
        savingText = UIManager.Instance.savingText;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            worldSave.SaveFragment();
            worldSave.SavePillar();
            worldSave.SaveGameObjectActivation();
            worldSave.SaveCorruptionCore();
            playerSave.Save();

            if (savingText.activeSelf == false)
            {
                savingText.SetActive(true);
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (savingText.activeSelf == true)
            {
                savingText.SetActive(false);
            }
        }
    }
}
