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
        //Scene currentScene;
        private string saveName;

        public Intro_Manager introManager;

        public void ResumeGame()
        {
            //LevelManager.Instance.LaunchGame();

            if (PlayerPrefs.HasKey("scene"))
            {
                Debug.Log("scene Detected");
                saveName = PlayerPrefs.GetString("scene");
                SceneManager.LoadScene(saveName);

                
                //PlayerManager.Instance.LoadPlayer();
            }
            else
            {
                Debug.Log("Don't have any save");
            }
        }

        public void NewGame()
        {
            //Emmène sur la scene qui se trouve après la scene en cours

            //Fenetre de confimation

            PlayerPrefs.DeleteAll();
            introManager.StartCinematic();
            //SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));

        }

        public void QuitGame()
        {
            Debug.Log("You Quit");
            Application.Quit();
        }
    }
}

