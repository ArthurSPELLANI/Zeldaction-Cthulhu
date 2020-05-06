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

                //Stock la vie du joueur
                PlayerPrefs.SetInt("life", PlayerManager.Instance.playerStats.playerCurrentHealth);

                //Stock le nombre de balles et le nombre de heal dans l'inventaire du joueur
                PlayerPrefs.SetInt("bulletNumber", PlayerManager.Instance.playerShoot.ammunitions);
                PlayerPrefs.SetInt("healNumber", PlayerManager.Instance.playerStats.healNumber);

                //Stock le fait que le joueur puisse utiliser son ombre
                if (PlayerManager.Instance.playerShadowMode.enabled)
                {
                    PlayerPrefs.SetInt("canUseShadow", 1);
                    PlayerPrefs.SetFloat("maxSanity", PlayerManager.Instance.playerShadowMode.maxSanity);
                    PlayerPrefs.SetInt("maxActionPoints", PlayerManager.Instance.playerShadowMode.maxActionPoints);
                    PlayerPrefs.SetInt("fragementNumbre", PlayerManager.Instance.playerShadowMode.fragment);
                }
                else if (!PlayerManager.Instance.playerShadowMode.enabled)
                {
                    PlayerPrefs.SetInt("canUseShadow", 0);
                }

                //Stock le fait que le joueur puisse utiliser sa machette
                if (PlayerManager.Instance.playerAttack.enabled)
                {
                    PlayerPrefs.SetInt("canUseMachette", 1);
                }
                else if (!PlayerManager.Instance.playerAttack.enabled)
                {
                    PlayerPrefs.SetInt("canUseMachette", 0);
                }

                //Stock le fait que le joueur puisse utiliser son gun
                if (PlayerManager.Instance.playerShoot.enabled)
                {
                    PlayerPrefs.SetInt("canUsGun", 1);
                }
                else if (!PlayerManager.Instance.playerShoot.enabled)
                {
                    PlayerPrefs.SetInt("canUseGun", 0);
                }

                //toutes les variables sont stockés dans les playerprefs
                PlayerPrefs.Save();
            }
        }
    }
}

