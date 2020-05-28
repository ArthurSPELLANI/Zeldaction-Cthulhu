﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Game;
using Player;
using Management;


namespace Menu
{
    public class PauseMenu : Singleton<PauseMenu>
    {
        public static bool gameIsPaused = false;
        public GameObject PauseMenuUI;
        GameObject shadowModeGo;
        GameObject attackBehaviorGo;
        GameObject movementBehaviorGo;

        public GameObject Save;
        public WorldSave worldSaveScript;
        public PlayerSave playerSaveScript;

        public GameObject myButton;

        private void Awake()
        {
            MakeSingleton(true);
        }

        void Start()
        {
            shadowModeGo = PlayerManager.Instance.playerShadowMode.gameObject;
            attackBehaviorGo = PlayerManager.Instance.playerAttack.gameObject;
            movementBehaviorGo = PlayerManager.Instance.playerMovement.gameObject;
        }

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

            if(shadowModeGo.GetComponent<PlayerShadowMode>().isShadowActivated == true)
            {
                Time.timeScale = 0.02f;
            }
            else
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
            if (Save != null)
            {
                worldSaveScript = Save.GetComponent<WorldSave>();
                playerSaveScript = Save.GetComponent<PlayerSave>();
                playerSaveScript.Save();
                worldSaveScript.SavePillar();
                worldSaveScript.SaveFragment();
            }
            

            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("You Quit");

            if (Save != null)
            {
                worldSaveScript = Save.GetComponent<WorldSave>();
                playerSaveScript = Save.GetComponent<PlayerSave>();
                playerSaveScript.Save();
                worldSaveScript.SavePillar();
                worldSaveScript.SaveFragment();
            }

                Application.Quit();
        }
    }
}

