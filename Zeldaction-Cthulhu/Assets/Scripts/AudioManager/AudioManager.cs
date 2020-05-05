using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Management;


/*pour ajouer un son quelque part ajputer cette ligne à l'endroit voulu :
FindObjectOfType<AudioManager>().Play("name");
Si l'audiomanager est en singleton c'est : 
AudioManager.Instance.Play("name");*/
namespace AudioManaging
{


    public class AudioManager : Singleton<AudioManager>
    {
        [Range(0f, 2f)]
        public float volumeSounds;

        [Range(0f, 2f)]
        public float volumeMusics; 

        public bool walkOnPierre;
        public bool walkOnHerbe;
        public bool walkOnPlancher;

        public bool bruitBresson;

        public Sound[] sounds;
        public Sound[] pasPierre;
        public Sound[] pasHerbe;
        public Sound[] pasPlancher;
        public Sound[] currentPas;
        public Sound[] BruitsSombes;

        AudioReverbZone Zone;

        public int negativeEffectPalier;
        void Awake()
        {
            MakeSingleton(true);

            volumeSounds = 1f;
            volumeMusics = 1f;

            Zone = GetComponent<AudioReverbZone>();

            #region Création d'audioSource
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volumeSounds;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volumeSounds;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            foreach (Sound s in pasPierre)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volumeSounds;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volumeSounds;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            foreach (Sound s in pasHerbe)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volumeSounds;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volumeSounds;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            foreach (Sound s in pasPlancher)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volumeSounds;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volumeSounds;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            foreach (Sound s in BruitsSombes)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * volumeSounds;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                if (s.volume == 0)
                    s.source.volume = 0.5f * volumeSounds;
                if (s.pitch == 0)
                    s.source.pitch = 1f;
            }
            #endregion

        }
        void Update()
        {
            if (walkOnPierre && currentPas != pasPierre)
                currentPas = pasPierre;
            if (walkOnHerbe && currentPas != pasHerbe)
                currentPas = pasHerbe;
            if (walkOnPlancher && currentPas != pasPlancher)
                currentPas = pasPlancher;

            if (bruitBresson)
                PlayBresson();

            if (negativeEffectPalier > 1)
                NegativeZone();
            else
                ThingsGoNormal();

        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (negativeEffectPalier == 1)
            {
                s.source.pitch = Random.Range(0.8f, 1.2f);
            }
            else if (negativeEffectPalier == 2)
            {
                s.source.pitch = Random.Range(0.5f, 1.5f);
            }
            else if (negativeEffectPalier == 3)
            {
                s.source.pitch = Random.Range(0.2f, 2f);
            }

            s.source.Play();
        }
        public void CoursePierre()
        {
            Sound s = currentPas[Mathf.RoundToInt(Random.value * (currentPas.Length - 1))];
            s.source.Play();
        }
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }
        void ThingsGoNormal()
        {
            foreach (Sound s in sounds)
            {
                s.source.pitch = s.pitch;
            }
            Zone.enabled = false;
        }
        
        
        private float currentTime = 0;
        float randomTime;
        AudioSource currentBresson;
        bool first = true;

        void PlayBresson()
        {
            if (first)
            {
                Sound s = BruitsSombes[Mathf.RoundToInt(Random.value * (BruitsSombes.Length - 1))];
                s.source.Play();
                currentBresson = s.source;
                first = false;
            }
               
            currentTime += Time.deltaTime;

            if (currentTime >= (currentBresson.clip.length + randomTime) && !first)
            {
                Sound s = BruitsSombes[Mathf.RoundToInt(Random.value * (BruitsSombes.Length - 1))];
                s.source.Play();
                currentTime = 0;
                randomTime = Random.Range(10f, 20f);
                currentBresson = s.source;
            }          
        }
        void NegativeZone()
        {
            Zone.enabled = true;
            if (negativeEffectPalier == 2)
            {
                Zone.room = -500;
                Zone.reverb = 700;
            }
            if (negativeEffectPalier == 3)
            {
                Zone.reverb = 1500;
                Zone.room = -100;
            }
        }    
    }
}
