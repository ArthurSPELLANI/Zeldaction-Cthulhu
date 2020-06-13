using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace Game
{
    public class FadeSwitchScene : MonoBehaviour
    {
        public Image fond;
        int sceneIndex;
        LevelManager lm;


        private void OnEnable()
        {
            //StartCoroutine(FadeToIn(1f, 0.7f));
        }

        // Start is called before the first frame update
        void Start()
        {
            lm = LevelManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator FadeToIn(float aValue, float aTime, int sIndex)
        {
            //Debug.Log("debut fade");
            float alpha = fond.color.a;

            for (float t = 0.0f; t < 2.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
                fond.color = newColor;

                yield return null;
            }

            if (sIndex == 0)
            {
                lm.Town();
            }
            if (sIndex == 1)
            {
                lm.InnInterior();
            }
            if (sIndex == 2)
            {
                lm.UpstairInn();
            }
            if (sIndex == 3)
            {
                lm.Bedroom();
            }
            if (sIndex == 4)
            {
                lm.UpstairInnNight();
            }
            if (sIndex == 5)
            {
                lm.InnInteriorNight();
            }
            if (sIndex == 6)
            {
                lm.TownNight();
            }
            if (sIndex == 7)
            {
                lm.LightForest1();
            }
            if (sIndex == 8)
            {
                lm.LightForest2();
            }
            if (sIndex == 9)
            {
                lm.DarkForest();
            }
            if(sIndex  == 10)
            {
                lm.Cave();
            }
            if(sIndex == 11)
            {
                lm.CorruptedForest();
            }
            if(sIndex == 12)
            {
                lm.TownDestroyed();
            }
            if(sIndex == 21)
            {
                lm.CaveWest();
            }
            if(sIndex == 22)
            {
                lm.CaveEast();
            }
            if(sIndex == 31)
            {
                lm.WestDF();
            }
            if(sIndex == 32)
            {
                lm.SouthDF();
            }
            if(sIndex == 33)
            {
                lm.EastDF();
            }
        }

        public IEnumerator FadeToOut(float aValue, float aTime)
        {
            //Debug.Log("debut fade");
            float alpha = fond.color.a;
            for (float t = 0.0f; t < 2.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
                fond.color = newColor;

                yield return null;
            }
        }
    }
}


