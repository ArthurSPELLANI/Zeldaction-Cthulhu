using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void ResumeGame()
        {
            //SceneManager.LaodScene(LaSceneSauvegardé)
        }

        public void NewGame()
        {
            //Emmène sur la scene qui se trouve après la scene en cours
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

