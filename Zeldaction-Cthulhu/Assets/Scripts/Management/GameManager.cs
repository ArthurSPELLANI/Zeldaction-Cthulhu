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

        void Awake()
        {
            MakeSingleton(true);

            globalLight = GetComponentInChildren<Light2D>();
        }

        void Start()
        {

        }


        void Update()
        {

            if(PlayerManager.Instance.playerShadowMode.sanity < PlayerManager.Instance.playerShadowMode.maxSanity)

            globalLight.intensity = PlayerManager.Instance.playerShadowMode.sanity / PlayerManager.Instance.playerShadowMode.maxSanity;
        }



    }
}