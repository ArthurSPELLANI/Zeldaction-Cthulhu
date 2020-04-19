using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSave : MonoBehaviour
    {
        public GameObject player;

        void DeleteSaves()
        {
            PlayerPrefs.DeleteAll();
        }

        void Save()
        {
            //Stock la position du joueur
            PlayerPrefs.SetFloat("positionX", player.transform.position.x);
            PlayerPrefs.SetFloat("positionY", player.transform.position.y);

            //Stock le nombre de balles et le nombre de heal dans l'inventaire du joueur
            PlayerPrefs.SetInt("bulletNumber", PlayerManager.Instance.playerShoot.ammunitions);
            PlayerPrefs.SetInt("healNumber", PlayerManager.Instance.playerStats.healNumber);

            //Stock toutes les variables relatives 
            /*if (bool canUseShadow ou qlqc comme ça)
            {
                PlayerPrefs.SetInt("canUseShadow", 1);
            }*/

            //stock le fait que le joueur ait acces à la machette ou non
            /*if (bool canUseMachette ou qlqc comme ça)
            {
                PlayerPrefs.SetInt("canUseMachette", 1);
            }*/

            //Référence les Cristaux d'ombre toujours récupérable par le joueur
            
        }
    }
}

