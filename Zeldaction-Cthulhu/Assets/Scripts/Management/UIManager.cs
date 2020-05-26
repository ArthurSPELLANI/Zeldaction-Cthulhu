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
        public GameObject gameOver;


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

            if (PlayerManager.Instance.playerShadowMode.maxSanity == 90)
            {
                SanityLevel3();
            }

        }

        public void SanityLevel1()
        {
            jaugeMid1.gameObject.SetActive(false);
            jaugeMid2.gameObject.SetActive(false);
            jaugeEnd.rectTransform.anchoredPosition = new Vector2(48.8f, 390.5f);
            fill.rectTransform.offsetMax = new Vector2(-58.5f, -4.5f);
            sanityGauge.maxValue = 60;
            sanityGauge.minValue = -40;
        }

        public void SanityLevel2()
        {
            jaugeMid1.gameObject.SetActive(true);
            jaugeMid2.gameObject.SetActive(false);
            jaugeEnd.rectTransform.anchoredPosition = new Vector2(66.8f, 390.5f);
            fill.rectTransform.offsetMax = new Vector2(-33.5f, -4.5f);
            sanityGauge.maxValue = 75;
            sanityGauge.minValue = -25;
        }

        public void SanityLevel3()
        {
            jaugeMid1.gameObject.SetActive(true);
            jaugeMid2.gameObject.SetActive(true);
            jaugeEnd.rectTransform.anchoredPosition = new Vector2(84.2f, 390.5f);
            fill.rectTransform.offsetMax = new Vector2(-8.5f, -4.5f);
            sanityGauge.maxValue = 90;
            sanityGauge.minValue = -10;
        }
        
        public void ContinueGameOver()
        {
            LevelManager.Instance.LaunchGame();
        }

        public void MenuGameOver()
        {

        }

    }
}

