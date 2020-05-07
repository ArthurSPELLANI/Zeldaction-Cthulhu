using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Game;
using Player;


namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public GameObject PauseMenuUI;
        public PlayerSave playerSaveScript;
        public GameObject shadowModeGo;
        public GameObject attackBehaviorGo;
        public GameObject movementBehaviorGo;

        //public WorldSave worldSaveScript;

        public GameObject myButton;

        void Update()
        {
            //changer pour start
            if (Input.GetButtonDown("Start"))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            shadowModeGo.SetActive(true);
            attackBehaviorGo.GetComponent<PlayerAttack>().cantAttack = false;
            attackBehaviorGo.GetComponent<PlayerShoot>().canShoot = true;
            movementBehaviorGo.GetComponent<PlayerMovement>().canMove = true;
        }

        void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            EventSystem.current.SetSelectedGameObject(myButton);
            shadowModeGo.SetActive(false);
            attackBehaviorGo.GetComponent<PlayerAttack>().cantAttack = true;
            attackBehaviorGo.GetComponent<PlayerShoot>().canShoot = false;
            movementBehaviorGo.GetComponent<PlayerMovement>().canMove = false;
        }

        public void LoadMenu()
        {

            playerSaveScript.Save();
            //worldSaveScript.SavePillar();
            //worldSaveScript.SaveFragment();

            Time.timeScale = 1f;
            SceneManager.LoadScene("TestMenu");
        }

        public void QuitGame()
        {
            Debug.Log("You Quit");

            playerSaveScript.Save();
            //worldSaveScript.SavePillar();
            //worldSaveScript.SaveFragment();

            Application.Quit();
        }
    }
}

