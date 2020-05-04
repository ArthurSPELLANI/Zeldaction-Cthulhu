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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayestHub();
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
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;            
        }

        public void LightForest1()
        {
            SceneManager.LoadScene("Light_Forest_1");
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
            PlayerManager.Instance.transform.position = PlayerManager.Instance.nativePosition;
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




    }
}


