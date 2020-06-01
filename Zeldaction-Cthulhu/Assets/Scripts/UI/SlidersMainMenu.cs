using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManaging;

namespace Menu
{
    public class SlidersMainMenu : MonoBehaviour
    {
        Slider SfxSlider;
        float check1;
        Slider MusicSlider;
        float check2;

        public bool isMainMenu;

        void Start()
        {

            SfxSlider = transform.GetChild(1).gameObject.GetComponent<Slider>();
            MusicSlider = transform.GetChild(2).gameObject.GetComponent<Slider>();

            SfxSlider.maxValue = 2;
            SfxSlider.minValue = 0;
            SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1);

            MusicSlider.maxValue = 2;
            MusicSlider.minValue = 0;
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        }

        void Update()
        {
            if (!isMainMenu)
            {
                if (SfxSlider.value != AudioManager.Instance.volumeSounds)
                {
                    AudioManager.Instance.volumeSounds = SfxSlider.value;
                    PlayerPrefs.SetFloat("SfxVolume", SfxSlider.value);

                }
                if (MusicSlider.value != AudioManager.Instance.volumeMusics)
                {
                    AudioManager.Instance.volumeMusics = MusicSlider.value;
                    PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
                }
            }
            if (isMainMenu)
            {
                if (SfxSlider.value != check1)
                {
                    check1 = SfxSlider.value;
                    PlayerPrefs.SetFloat("SfxVolume", SfxSlider.value);

                }
                if (MusicSlider.value != check2)
                {
                    check2 = MusicSlider.value;
                    PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
                }
            }
            
        }
    }


}
