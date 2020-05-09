using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Management;
using Player;

namespace Game
{
    public class LevelManager : Singleton<LevelManager>
    {
        private void Awake()
        {
            MakeSingleton(true);
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayestHub();
            }*/
        }

        //Fonction à lancer au début du jeu pour utiliser les sauvegardes
        Scene currentScene;
        float posX;
        float posY;
        public void LaunchGame()
        {
            if (PlayerPrefs.HasKey("Scene"))
            {
                currentScene.name = PlayerPrefs.GetString("scene");
                SceneManager.LoadScene("scene");

                posX = PlayerPrefs.GetFloat("positionX");
                posY = PlayerPrefs.GetFloat("positionY");
                PlayerManager.Instance.transform.position = new Vector3(posX, posY, 0f);
                PlayerManager.Instance.LoadPlayer();
            }
            else
            {
                SceneManager.LoadScene("01_Town"); //je connais pas le nom de la première scene
                PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
                PlayerManager.Instance.ResetPlayer();
            }

        }

        public void PlayestHub()
        {
            SceneManager.LoadScene("Playtest_Hub");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
            PlayerManager.Instance.ResetPlayer();
        }

        public void DarkForest()
        {
            SceneManager.LoadScene("10_DarkForest");
            PlayerManager.Instance.transform.position = new Vector3(6, -23);           
        }

        public void LightForest1()
        {
            SceneManager.LoadScene("08_LightForest1");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void LightForest2()
        {
            SceneManager.LoadScene("09_LightForest2");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void EnemyRoom()
        {
            SceneManager.LoadScene("Enemy_Room");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void Cave()
        {
            SceneManager.LoadScene("11_Cave");
            PlayerManager.Instance.transform.position = new Vector3(0.9f, -8);
        }

        public void InnInterior()
        {
            SceneManager.LoadScene("02_InnInterior");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void UpstairInn()
        {
            SceneManager.LoadScene("03_UpstairInn");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void Bedroom()
        {
            SceneManager.LoadScene("04_Bedroom");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void UpstairInnNight()
        {
            SceneManager.LoadScene("05_UpstairInnNight");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void InnInteriorNight()
        {
            SceneManager.LoadScene("06_InnInteriorNight");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void TownNight()
        {
            SceneManager.LoadScene("07_TownNight");
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
        }

        public void CorruptedForest()
        {
            SceneManager.LoadScene("12_CorruptedForest");
            PlayerManager.Instance.transform.position = new Vector3 (6, -24);
        }

        public void TownDestroyed()
        {
            SceneManager.LoadScene("13_TownDestroyed");
            PlayerManager.Instance.transform.position = new Vector3(0.6f, 6);
        }



    }
}


