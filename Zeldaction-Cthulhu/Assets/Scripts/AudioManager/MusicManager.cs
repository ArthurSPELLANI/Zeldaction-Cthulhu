using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource startMusic;
        public AudioSource loopMusic;
        public AudioSource combatMusic;
        public bool isOnFight;

        public float volume;

        static float time;

        float distortion1 = 1f;
        float distortion2 = 1f;

        [Range(0.05f, 0.5f)]
        public float distotionValue = 0.1f;

        float distortionTime;
        float distortionSpeed;

        int soundRight;
        int soundLeft;

        bool isSwitchingDistortion;

        void Start()
        {
            volume = 0.5f * PlayerPrefs.GetFloat("MusicVolume", 1);

            if (startMusic != null)
                startMusic.volume = volume;

            loopMusic.volume = volume;

            if (combatMusic != null)
                combatMusic.volume = 0f;

            time = 0f;

            if (startMusic != null)
            {
                startMusic.Play();
                StartCoroutine(EndOfTheStart());
            }
            else
            {
                loopMusic.Play();

                if (combatMusic != null)
                    combatMusic.Play();
            }


           
        }

        void Update()
        {
            if (AudioManager.Instance.volumeMusics != AudioManager.Instance.checkVM)
            {
                AudioManager.Instance.checkVM = AudioManager.Instance.volumeMusics;
                SwitchVolumeMusic();
            }

            //déclanchement/arret de la musique de combat
            if (isOnFight && combatMusic.volume != volume)
                combatMusic.volume = Mathf.Lerp(0f, volume, time);

            if (combatMusic != null)
            {
                if (!isOnFight && combatMusic.volume != 0f)
                    combatMusic.volume = Mathf.Lerp(volume, 0f, time);
            }

            if (time > 1f)
            {
                time = 0f;
            }

            time += 0.2f * Time.deltaTime;

            if (AudioManager.Instance.negativeEffectPalier > 0)
            {
                NegativeEffects();


                if (distortionTime < 1f)
                {
                    if (combatMusic != null)
                        combatMusic.pitch = Mathf.Lerp(distortion1, distortion2, distortionTime);

                    loopMusic.pitch = Mathf.Lerp(distortion1, distortion2, distortionTime);

                    distortionTime += (1 / distortionSpeed) * Time.deltaTime;
                }
                else
                {
                    distortionTime = 0f;
                    SwitchDistortion(AudioManager.Instance.negativeEffectPalier);
                }
            }
                
            else
                ThingsGoNormal();
        }
        IEnumerator EndOfTheStart()
        {
            yield return new WaitForSecondsRealtime(startMusic.clip.length);
            loopMusic.Play();

            if (combatMusic != null)
                combatMusic.Play();
        }

        void NegativeEffects()
        {
            if (startMusic != null)
                startMusic.bypassReverbZones = false;

            if (combatMusic != null)
                combatMusic.bypassReverbZones = false;

            loopMusic.bypassReverbZones = false;
        }
        void ThingsGoNormal()
        {
            if (startMusic != null)
            {
                startMusic.bypassReverbZones = true;
                startMusic.pitch = 1;
            }

            if (combatMusic != null)
            {
                combatMusic.bypassReverbZones = true;
                combatMusic.pitch = 1;
            }
                

            loopMusic.bypassReverbZones = true;
            loopMusic.pitch = 1;
        }

        void SwitchDistortion(int pallier)
        {
            distortionSpeed = Random.Range(3f, 10f);
            distortion1 = distortion2;
            distortion2 = (pallier * distotionValue) * Random.Range(-1f, 1f) + 1;
        }

        void SwitchVolumeMusic()
        {
            volume = 0.5f * AudioManager.Instance.volumeMusics;

            if (startMusic != null)
                startMusic.volume = volume;

            if (combatMusic != null)
                combatMusic.volume = volume;

            loopMusic.volume = volume;
        }
    }

}
