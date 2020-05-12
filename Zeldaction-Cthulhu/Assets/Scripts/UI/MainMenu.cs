using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Game;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {

        public void ResumeGame()
        {
            LevelManager.Instance.LaunchGame();
        }

        public void NewGame()
        {
            //Emmène sur la scene qui se trouve après la scene en cours

            //Fenetre de confimation

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //Reset la sauvegarde du joueur
        }

        public void QuitGame()
        {
            Debug.Log("You Quit");
            Application.Quit();
        }
    }
}

