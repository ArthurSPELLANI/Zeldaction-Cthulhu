using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Management;


/*pour ajouer un son quelque part ajputer cette ligne à l'endroit voulu :
FindObjectOfType<AudioManager>().Play("name");*/
namespace AudioManaging
{


    public class AudioManager : Singleton<AudioManager>
    {
        public Sound[] sounds;
        public Sound[] pasPierre;
        float currentTime;

        int index;
        void Awake()
        {
            MakeSingleton(true);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            foreach (Sound s in pasPierre)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }

        }
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
        public void CoursePierre()
        {
            Sound s = pasPierre[Mathf.RoundToInt(Random.value * (pasPierre.Length - 1))];
            s.source.Play();
        }
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }
        /*void Start()
        {
            PlayMusic();
        }
        void Update()
        {
            currentTime += Time.deltaTime;
        }
        public void Course()
        {
           Sound s = pas[Mathf.RoundToInt(Random.value * (pas.Length - 1))];
           s.source.Play();
        }
        public void PlayMusic()
        {
            Sound s = music[Mathf.RoundToInt(Random.value * (music.Length - 1))];
            s.source.Play();
            if (currentTime > s.clip.length)
            {
                PlayMusic();
                currentTime = 0;
            }
        }
        #region Sound
        public float timeBetweenStep = 0.1f;
        private float currentTime = 0;
        void SoundRunning()
        {
            if (playerrgb.velocity != Vector2.zero)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= timeBetweenStep)
                {
                    FindObjectOfType<AudioManager>().Course();
                    currentTime = 0;
                }
            }           
        }
        #endregion*/
    }
}
