using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public class AudioNegaFx : MonoBehaviour
    {
        public AudioSource Voice;
        public Sound[] Voices;
        public AudioSource soundHorror;

        float pan1;
        float pan2;
        [Range(0.1f, 0.5f)]
        float panValue = 0.3f;
        float panTime;
        float panSpeed;

        float volume;

        float time;

        void Start()
        {
            volume = 0.5f * AudioManager.Instance.volumeSounds;
            soundHorror.volume = volume;

            foreach (Sound s in Voices)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volume;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
        }

        void Update()
        {
            if (AudioManager.Instance.volumeSounds != AudioManager.Instance.checkVS)
            {
                AudioManager.Instance.checkVS = AudioManager.Instance.volumeSounds;
                volume = 0.5f * AudioManager.Instance.volumeSounds;
                SwitchVolume();
            }

            if (AudioManager.Instance.negativeEffectPalier > 1)
            {
                PlayVoices();

                if (!Voice.isPlaying)
                {
                    StartCoroutine(Aie());
                    Voice.Play();
                }

                if (Voice.volume != volume)
                    Voice.volume = Mathf.Lerp(0f, volume, time);


                if (time > 1f && Voice.volume != volume)
                {
                    time = 0f;
                }

                time += 0.2f * Time.deltaTime;

                if (panTime < 1f)
                {

                    Voice.panStereo = Mathf.Lerp(pan1, pan2, panTime);

                    panTime += (1 / panSpeed) * Time.deltaTime;
                }
                else
                {
                    panTime = 0f;
                    SwitchPan(AudioManager.Instance.negativeEffectPalier);
                }
            }
            else 
            {
                if (Voice.isPlaying && Voice.volume != 0)
                {
                    Voice.volume = Mathf.Lerp(volume, 0f, time);

                    if (time > 1f)
                    {
                        time = 0f;
                    }
                }
                else if (Voice.isPlaying && Voice.volume == 0)
                {
                    Voice.Stop();
                }
            }
        }

        private float currentTime = 0;
        float randomTime;
        AudioSource currentVoice;
        bool first = true;

        void PlayVoices()
        {
            if (first)
            {
                
                Sound s = Voices[Mathf.RoundToInt(Random.value * (Voices.Length - 1))];
                s.source.panStereo = Mathf.RoundToInt(Random.Range(-1, 1));
                s.source.volume = volume;
                s.source.Play();
                currentVoice = s.source;
                randomTime = Random.Range(3f, 8f);
                first = false;
            }

            currentTime += Time.deltaTime;

            if (currentTime >= (currentVoice.clip.length + randomTime) && !first)
            {
                Sound s = Voices[Mathf.RoundToInt(Random.value * (Voices.Length - 1))];
                s.source.panStereo = Mathf.RoundToInt(Random.Range(-1, 1));
                s.source.volume = volume;
                s.source.Play();
                currentTime = 0;
                randomTime = Random.Range(3f, 8f);
                currentVoice = s.source;
            }
        }
        void SwitchPan(int pallier)
        {
            panSpeed = Random.Range(2f, 7f);
            pan1 = pan2;
            pan2 = (pallier * panValue) * Random.Range(-1f, 1f);
        }

        float AieTime = 5f;
        IEnumerator Aie()
        {
            yield return new WaitForSecondsRealtime(AieTime);
            soundHorror.Play();
            AieTime = Random.Range(5f, 10f) + soundHorror.clip.length;

            if (AudioManager.Instance.negativeEffectPalier > 1)
            {
                StartCoroutine(Aie());
            }

        }

        void SwitchVolume()
        {
            Voice.volume = volume;
            soundHorror.volume = volume;
        }
    }

}
