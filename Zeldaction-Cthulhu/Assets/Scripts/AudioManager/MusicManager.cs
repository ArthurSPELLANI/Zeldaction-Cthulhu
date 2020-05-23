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
            volume = 0.5f * AudioManager.Instance.volumeMusics;
            startMusic.volume = volume;
            loopMusic.volume = volume;
            time = 0f;           
            startMusic.Play();
            StartCoroutine(EndOfTheStart());
        }

        void Update()
        {
            //déclanchement/arret de la musique de combat
            if (isOnFight && combatMusic.volume != volume)
                combatMusic.volume = Mathf.Lerp(0f, volume, time);

            if (!isOnFight && combatMusic.volume != 0f)
                combatMusic.volume = Mathf.Lerp(volume, 0f, time);

            if (time > 1f)
            {
                time = 0f;
            }

            time += 0.2f * Time.deltaTime;

            if (AudioManager.Instance.negativeEffectPalier > 0)
            {
                NegativeEffects();

                if (!isSwitchingDistortion)
                    StartCoroutine(SwitchDistortion(AudioManager.Instance.negativeEffectPalier));

                combatMusic.pitch = Mathf.Lerp(distortion1, distortion2, distortionTime);
                loopMusic.pitch = Mathf.Lerp(distortion1, distortion2, distortionTime);

                distortionTime += (1 / distortionSpeed) * Time.deltaTime;

                if (distortionTime > 1f)
                {
                    distortionTime = 0f;
                }
            }
                
            else
                ThingsGoNormal();
        }
        IEnumerator EndOfTheStart()
        {
            yield return new WaitForSecondsRealtime(startMusic.clip.length);
            loopMusic.Play();
            combatMusic.Play();
        }

        void NegativeEffects()
        {
            startMusic.bypassReverbZones = false;
            combatMusic.bypassReverbZones = false;
            loopMusic.bypassReverbZones = false;
        }
        void ThingsGoNormal()
        {
            startMusic.bypassReverbZones = true;
            combatMusic.bypassReverbZones = true;
            loopMusic.bypassReverbZones = true;
        }

        IEnumerator SwitchDistortion(int pallier)
        {
            isSwitchingDistortion = true;

            yield return new WaitForSecondsRealtime(distortionSpeed);

            distortionSpeed = Random.Range(3f, 10f);
            distortion1 = distortion2;
            distortion2 = (pallier * distotionValue) * Random.Range(-1f, 1f) + 1;

            if (AudioManager.Instance.negativeEffectPalier > 0)
                StartCoroutine(SwitchDistortion(AudioManager.Instance.negativeEffectPalier));
            else
                isSwitchingDistortion = false;
            
        }
    }

}
