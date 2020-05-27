using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AudioManaging
{
    public class AudioManagScene : MonoBehaviour
    {
        AudioManager AM;

        Scene CurrentScene;
        Scene NewScene;
        
        void Awake()
        {
            AM = GetComponent<AudioManager>();
            NewScene = SceneManager.GetActiveScene();
        }

        void Update()
        {
            if (CurrentScene != NewScene)
            {
                CurrentScene = NewScene;
                SwitchScene();
            }
        }

        void SwitchScene() // oui je sais c'est dégueulasse
        {
            AM.walkOnHerbe = false;
            AM.walkOnPierre = false;
            AM.walkOnPlancher = false;

            if (CurrentScene.buildIndex == 1)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 2)
            {
                AM.walkOnPlancher = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 3)
            {
                AM.walkOnPlancher = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 4)
            {
                AM.walkOnPlancher = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 5)
            {
                AM.walkOnPlancher = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 6)
            {
                AM.walkOnPlancher = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 7)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 8)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 9)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = false;
            }
            else if (CurrentScene.buildIndex == 10)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = true;
            }
            else if (CurrentScene.buildIndex == 11)
            {
                AM.walkOnPierre = true;
                AM.bruitBresson = true;
            }
            else if (CurrentScene.buildIndex == 10)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = true;
            }
            else if (CurrentScene.buildIndex == 10)
            {
                AM.walkOnHerbe = true;
                AM.bruitBresson = true;
            }
        }
    }
}

