using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Management;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;

namespace Game
{
    /// <summary>
    /// General management of the game.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {

        [SerializeField] public Light2D globalLight;
        int fps = 60;


        void Awake()
        {
            MakeSingleton(true);
            globalLight = GetComponentInChildren<Light2D>();

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = fps;
        }

        void Start()
        {

        }


        void Update()
        {

            if(PlayerManager.Instance.playerShadowMode.sanity < PlayerManager.Instance.playerShadowMode.maxSanity)
            {
                globalLight.intensity = PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity;
            }

            if(Application.targetFrameRate != fps)
            {
                Application.targetFrameRate = fps;
            }
            
        }



    }
}