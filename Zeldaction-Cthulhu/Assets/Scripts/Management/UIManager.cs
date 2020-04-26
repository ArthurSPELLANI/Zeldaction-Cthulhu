using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Player;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : Singleton<UIManager>
    {

        public Image jaugeMid1;
        public Image jaugeMid2;
        public Image jaugeEnd;
        public Image fill;
        public Slider sanityGauge;


        private void Awake()
        {
            MakeSingleton(true);
        }


        private void Update()
        {
            if (PlayerManager.Instance.playerShadowMode.maxSanity == 60)
            {
                SanityLevel1();
            }

            if (PlayerManager.Instance.playerShadowMode.maxSanity == 75)
            {
                SanityLevel2();
            }
        }

        public void SanityLevel1()
        {
            jaugeMid1.gameObject.SetActive(false);
            jaugeMid2.gameObject.SetActive(false);
            jaugeEnd.rectTransform.anchoredPosition = new Vector2(43, 392.74f);
            fill.rectTransform.offsetMax = new Vector2(-63f, -4.5f);
            sanityGauge.maxValue = 60;
            sanityGauge.minValue = -40;
        }

        public void SanityLevel2()
        {
            jaugeMid1.gameObject.SetActive(true);
            jaugeMid2.gameObject.SetActive(false);
            jaugeEnd.rectTransform.anchoredPosition = new Vector2(60, 392.74f);
            fill.rectTransform.offsetMax = new Vector2(-39f, -4.5f);
            sanityGauge.maxValue = 75;
            sanityGauge.minValue = -25;
        }

    }
}

