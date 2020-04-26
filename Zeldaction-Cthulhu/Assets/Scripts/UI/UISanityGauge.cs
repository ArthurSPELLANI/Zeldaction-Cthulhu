using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

namespace UI
{
    public class UISanityGauge : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        float maxSanity;
        float sanity;

        private void Start()
        {
            maxSanity = PlayerManager.Instance.playerShadowMode.maxSanity;
            sanity = PlayerManager.Instance.playerShadowMode.sanity;
        }        

        public void SetMaxSanity(float sanity)
        {
            slider.maxValue = maxSanity;
            slider.value = maxSanity;
            fill.color = gradient.Evaluate(1f);            
        }

        public void SetSanity(float sanity)
        {
            slider.value = sanity;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}


