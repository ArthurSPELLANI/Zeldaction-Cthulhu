using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerSave : MonoBehaviour
    {
        public GameObject player;
        Scene currentScene;
        public bool useSaves;

        //Delete toutes les sauvegardes
        public void DeleteSaves()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Les sauvegardes ont été effacés");
        }

        //fonction à appeler pour sauvegarder la progression du joueur
        public void Save()
        {
            if (useSaves)
            {

                //Stock la scene dans laquelle se trouve le joueur
                currentScene = SceneManager.GetActiveScene();
                PlayerPrefs.SetString("scene", currentScene.name);

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

                //toutes les variables sont stockés dans les playerprefs
                PlayerPrefs.Save();
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Save();
            }
        }
    }
}

